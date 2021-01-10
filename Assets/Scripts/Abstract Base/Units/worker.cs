using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worker : Unit
{
    private HashSet<GameObject> markedTiles = new HashSet<GameObject>();
    public int buildingRange = 3;
    public HashSet<Vector3Int> validTiles = new HashSet<Vector3Int>();
    
    

    public void clearMarked()
    {
        validTiles = new HashSet<Vector3Int>();
        foreach (GameObject go in markedTiles)
        {
            Destroy(go);
        }
        markedTiles = new HashSet<GameObject>();
    }
}
