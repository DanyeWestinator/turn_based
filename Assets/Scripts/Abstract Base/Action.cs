using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Action : MonoBehaviour
{
    [Header("Unit stuff")]
    public Unit unit;
    public Vector3Int center;
    

    [Header("Valid Tiles stuff")]
    public Color markColor;
    private Color originalColor;
    //all the tiles visited in the recursive calls
    private HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
    //an updated list of all valid tiles for this action
    private HashSet<Vector3Int> validTiles;
    public int CurrentMaxDepth;
    public int recursiveCalls = 0;
    
    //housekeeping
    private static Tilemap grid;

    


    //performs the action, whatever that may be
    public abstract void PerformAction();

    //marks all valid tiles for this action
    public void MarkValidTiles()
    {
        GameManager.ClearGrid();
        if (center != unit.currentPos || validTiles.Count == 0)
        {
            recursiveCalls = 0;
            visited = new HashSet<Vector3Int>();
            center = unit.currentPos;
            validTiles = UpdateValidTiles(center, CurrentMaxDepth);
        }
        print("" + gameObject + transform.parent.gameObject);
        foreach(Vector3Int tile in validTiles)
        {
            print("marking " + tile + " to be " + markColor.ToString());
            GameManager.SetGridTileColor(tile, markColor);
        }
        //mark valid tiles here
        //need recursive helper function(s)
    }

    private HashSet<Vector3Int> UpdateValidTiles(Vector3Int center, int depth)
    {
        HashSet<Vector3Int> toReturn = new HashSet<Vector3Int>();
        //print(IsValidTile(center));
        //print(depth);
        if (IsValidTile(center) == false || depth <= 0)
            return toReturn;
        recursiveCalls++;
        visited.Add(center);
        toReturn.Add(center);
        for (int i = -1; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3Int next = new Vector3Int(i, j, 0);
                toReturn.UnionWith(UpdateValidTiles(next, depth - 1));
            }
        }

        return toReturn;
    }

    public void SelectAction(bool toSet = true)
    {
        if (toSet)
            MarkValidTiles();
        else
            GameManager.ClearGrid();
    }

    

    private void Start()
    {
        validTiles = new HashSet<Vector3Int>();
        if (grid == null)
        {
            grid = GameObject.Find("Grid").transform.Find("grid").GetComponent<Tilemap>();
        }
        unit = transform.root.gameObject.GetComponent<Unit>();
    }


    //a basic IsValidTile function. Returns false if the object in the tile isn't null or the unit itself
    //also returns false if the tile has ever been visited
    private bool IsValidTile(Vector3Int pos)
    {
        if (GameManager.GetUnitAt(pos) != null && GameManager.GetUnitAt(pos) != unit)
            return false;
        if (GameManager.GetBuildingAt(pos) != null)
            return false;
        if (visited.Contains(pos))
            return false;
        return true;
    }
    
}
