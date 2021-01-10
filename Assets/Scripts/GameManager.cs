using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance = null;

    public Tilemap Environment;
    public Tilemap grid;

    public static HashSet<Building> buildings = new HashSet<Building>();
    private static Dictionary<Vector3Int, Building> buildingsWithCoords = new Dictionary<Vector3Int, Building>();

    public static HashSet<Unit> units = new HashSet<Unit>();
    private static Dictionary<Vector3Int, Unit> unitsWithCoords = new Dictionary<Vector3Int, Unit>();


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        Start();
    }

    private static Tilemap map;
    // Start is called before the first frame update
    void Start()
    {
        if (grid == null)
            grid = GameObject.Find("Grid").transform.Find("grid").GetComponent<Tilemap>();
        if (Environment == null)
            Environment = GameObject.Find("Grid").transform.Find("Environment").GetComponent<Tilemap>();
    }

    //gets the name of the sprite at a given tile
    public static string tileAt(Vector3Int pos, Tilemap map = null)
    {
        if (map == null)
        {
            map = instance.Environment;
        }
        try
        {
            map.GetSprite(pos).ToString();
        }
        catch (System.NullReferenceException)
        {
            return "";
        }
        return map.GetSprite(pos).ToString().Replace(" (UnityEngine.Sprite)", "");
    }

    //gets all the tiles in a tilemap
    public static HashSet<Vector3Int> getAllTiles(Tilemap tiles)
    {
        //gets all the tiles in a given tilemap
        HashSet<Vector3Int> filledTiles = new HashSet<Vector3Int>();
        for (int x = tiles.cellBounds.xMin; x < tiles.cellBounds.xMax; x++)
        {
            for (int y = tiles.cellBounds.yMin; y < tiles.cellBounds.yMax; y++)
            {
                if (tiles.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    filledTiles.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        return filledTiles;
    }

    //helper function to get a color with a changed alpha value
    public static Color SetAlpha(Color color, float alpha)
    {
        Color newColor = color;
        if (alpha >= 0 && alpha <= 1)
        {
            newColor.a = alpha;
        }
        else if (alpha > 1 && alpha <= 256)
        {
            newColor.a = (alpha / 256);
        }
        return newColor;
    }

    //returns the building at the given tile, null if no building
    public static Building GetBuildingAt(Vector3Int coord)
    {
        if (buildingsWithCoords.ContainsKey(coord) == false)
        {
            return null;
        }
        return buildingsWithCoords[coord];
    }
    public static Unit GetUnitAt(Vector3Int coord)
    {
        if (unitsWithCoords.ContainsKey(coord) == false)
            return null;
        return unitsWithCoords[coord];
    }

    public static bool isTileEmpty(Vector3Int coord)
    {
        if (unitsWithCoords.ContainsKey(coord) || buildingsWithCoords.ContainsKey(coord))
            return false;
        return true;
    }

    public static void MoveUnit(Vector3Int newCoord, Unit toMove)
    {
        if (isTileEmpty(newCoord) == false)
            throw new System.Exception("tile " + newCoord + " is not empty");
        if (toMove.currentPos != newCoord)
        {
            unitsWithCoords.Remove(toMove.currentPos);
            unitsWithCoords.Add(newCoord, toMove);
            toMove.MoveUnit(newCoord);
        }
        
    }

    public static void ClearGrid()
    {
        //print("clearing grid");
        foreach (Vector3Int tile in getAllTiles(instance.grid))
        {
            instance.grid.SetColor(tile, GameController.getOriginalColor());
        }
    }
    public static void SetGridTileColor(Vector3Int pos, Color newColor) { instance.grid.SetColor(pos, newColor);}
}
