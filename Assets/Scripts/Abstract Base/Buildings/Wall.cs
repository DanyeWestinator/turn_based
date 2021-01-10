using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Building
{
    public override bool isValidTile(Vector3Int coord)
    {
        throw new System.NotImplementedException();
    }
    public override HashSet<Vector3Int> validBuildingLocations(Vector3Int start, int range)
    {
        throw new System.NotImplementedException();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
