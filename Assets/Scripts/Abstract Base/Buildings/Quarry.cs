using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Quarry : Building
{
    public override bool isValidTile(Vector3Int coord)
    {
        /*
        //the nested for loops get the 8 spaces around coord
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector3Int testPos = new Vector3Int(coord.x + i, coord.y + j, 0);
                string testTile = GameManager.tileAt(testPos);

                //returns false if testTile is an empty string, or if the tile is not a quarry tile, or if the tile is already worked
                if (testTile == "" || GameManager.quarryTiles.Contains(testTile) == false)
                    return false;
                if (GameManager.isTiledWorked(testPos))
                {
                    return false;

                }

            }
        }
        */
        return true;
    }

    public override HashSet<Vector3Int> validBuildingLocations(Vector3Int start, int range)
    {
        //print("finding valid tiles " + range + " spaces from " + start);
        HashSet<Vector3Int> valid = new HashSet<Vector3Int>();
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                Vector3Int testPos = new Vector3Int(start.x + i, start.y + j, 0);
                if (isValidTile(testPos))
                    valid.Add(testPos);
                
            }
        }
        return valid;
    }

    private void placeBuilding(Vector3Int coord)
    {
        if (isValidTile(coord) == false)
        {
            print("tried to place a building at invalid tile " + coord);
            return;
        }
        //print("placing building at " + coord);
        gameObject.GetComponent<worker>().clearMarked();
        GameObject newBuilding = Instantiate(buildingPrefab);
        newBuilding.transform.parent = null;
        newBuilding.transform.position = coord;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector3Int testPos = new Vector3Int(coord.x + i, coord.y + j, 0);
                //GameManager.addWorkedTile(testPos);
                if (i != 0 || j != 0)
                {
                    GameObject worked = Instantiate(workedTile);
                    worked.transform.parent = newBuilding.transform;
                    testPos.z = -7;
                    worked.transform.position = testPos;
                }
                
                
                    
            }
        }

    }

}
