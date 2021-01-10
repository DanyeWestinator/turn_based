using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [Header("Boundaries")]
        private int minX = -25;
        private int maxX = 25;
        private int minY = -25;
        private int maxY = 25;
    public bool isSquare;
    public int size = 25;

    [Space, Header("Movement")]
    public float speed = 5f;
    public float zoomSpeed = 5f;
    public Vector2 zoomLimits = new Vector2(20, 130);
    private float currentZoom;

    public GameObject grid;
    public bool TurnOffGridWhenMoving = false;
    public bool cameraSnap = true;

    // Start is called before the first frame update
    void Start()
    {
        if (size < 0)
        {
            size = -1 * size;
        }
        if (isSquare)
        {
            minX = minY = -1 * size;
            maxX = maxY = size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ScrollZoom();
    }
    private void Move()
    {
        Vector3 pos = transform.position;
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if (TurnOffGridWhenMoving)
        {
            grid.GetComponent<TilemapRenderer>().enabled = (dir == Vector3Int.zero);
        }
        dir *= speed * Time.deltaTime;
        pos += dir;
        if (inRange(pos))
        {
            transform.position = pos;
        }

    }

    private void ScrollZoom()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            float zoomDelta = -1f * Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            float newZoom = gameObject.GetComponent<Camera>().orthographicSize + zoomDelta;
            if (newZoom > zoomLimits.x && newZoom < zoomLimits.y)
                gameObject.GetComponent<Camera>().orthographicSize += zoomDelta;
            
        }
    }


    bool inRange(Vector3 pos)
    {
        if (pos.x < minX || pos.x > maxX)
            return false;
        if (pos.y < minY || pos.y > maxY)
            return false;
        return true;

    }
}
