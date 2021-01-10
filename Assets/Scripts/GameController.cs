using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public Unit currentWorker;

    public static GameObject currentUnit = null;

    //hand cursor stuff
    [Header("Hand Stuff")]
    [SerializeField]
    private bool displayMouseCursor = false;
    public GameObject hand;
    private GameObject handImage_GO = null;
    private Vector3 lastHandPos = Vector3.zero;


    //grid stuff
    [Header("Grid Stuff")]
    private static Color originalColor;
    public Color hoverColor;
    public Tilemap grid;



    private void Start()
    {
        Cursor.visible = displayMouseCursor;
        handImage_GO = hand.transform.GetChild(0).gameObject;
        //setHandList();
        SetAllTileFlags(grid);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //placeBuilding();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            getNearestObject();
        }
        moveHand();
        if (Input.GetKeyDown(KeyCode.Tab))
            currentWorker.NextAction();
    }

    private void SetAllTileFlags(Tilemap grid, TileFlags newFlag = TileFlags.None)
    {
        originalColor = grid.GetColor(Vector3Int.zero);
        for (int i = -(grid.size.x); i < grid.size.x; i++)
        {
            for (int j = -grid.size.y; j < grid.size.y; j++)
            {
                Vector3Int current = new Vector3Int(i, j, 0);
                grid.SetTileFlags(current, newFlag);
            }
        }

    }

    void setHandList()
    {
        foreach(var v in Resources.LoadAll("HandIcons", typeof(UnityEngine.Sprite)))
        {
            print(v.ToString());
        }
    }

    void moveHand()
    {
        hand.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        Vector3 worldSpaceHandPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldSpaceHandPos.z = 0f;
        if (Vector3Int.FloorToInt(lastHandPos) != Vector3Int.FloorToInt(worldSpaceHandPos))
        {
            //print("setting new color at " + Vector3Int.FloorToInt(worldSpaceHandPos));
            grid.SetColor(Vector3Int.FloorToInt(lastHandPos), originalColor);
            grid.SetColor(Vector3Int.FloorToInt(worldSpaceHandPos), hoverColor);
        }

        lastHandPos = worldSpaceHandPos;
    }

    /*
    public void placeBuilding()
    {
        Vector3Int clickedPos = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        clickedPos.z = 0;
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            print("clicked over button");
            return;
        }
        if (currentUnit != null && currentWorker.validTiles.Contains(clickedPos))
        {
            //currentWorker.currentBuilding.placeBuilding(clickedPos);
        }
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(currentUnit);
        if (currentUnit == null)
        {
            currentUnit = collision.gameObject;
        }

        if (currentUnit.GetComponent<Unit>() != null)
        {
            //currentUnit.GetComponent<Unit>().SelectUnit(false);
            currentUnit = collision.gameObject;
            //currentUnit.GetComponent<Unit>().SelectUnit(true);
        }
            
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    //gets the nearest game object to a position
    public void getNearestObject()
    {
        Vector3 clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPos.z = -6f;
        
        transform.position = clickedPos;
    }
    
    IEnumerator turnOffCollider()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    public static Color getOriginalColor() { return originalColor; }
}
