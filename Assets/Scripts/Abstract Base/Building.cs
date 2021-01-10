using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    //in game units, and their current counts
    //stone is for buildings
    private int stone = 0;
    //everything needs gold
    private int gold = 0;
    //wood is for ships and seige weapons
    private int wood = 0;
    //food is for units
    private int food = 0;

    public int hitPoints;

    public GameObject validLocationMarker;
    public GameObject buildingPrefab;
    public GameObject workedTile;

    public GameObject cratePrefab;

    public bool canContainUnit = true;
    public bool currentlyContainsUnit = false;

    /// returns a hashset of all valid tiles in a certain range
    //public abstract HashSet<Vector3Int> validTiles(Vector3Int start, int range);

    public abstract HashSet<Vector3Int> validBuildingLocations(Vector3Int start, int range);

    //places a building at the given tile
    //assumes the spot is valid
    private void placeBuilding(Vector3Int coord) { }

    public void addBuilding(Vector3Int coord)
    {
        if (GameManager.GetBuildingAt(coord) == null)
            return;
        placeBuilding(coord);
    }

    public void DeleteBuilding()
    {

        Destroy(this.gameObject);
    }

    //determines if the tile is valid for the building
    public abstract bool isValidTile(Vector3Int coord);


    public int GetFood() { return food; }
    public int GetWood() { return wood; }
    public int GetGold() { return gold; }
    public int GetStone() { return stone; }
    public GameObject getValidTileMarker() { return validLocationMarker; }

}
