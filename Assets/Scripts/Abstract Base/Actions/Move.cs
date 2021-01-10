using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Move : Action
{

    public override void PerformAction()
    {
        print("placeholder moving");
    }


    /*public override void MarkValidTiles()
    {
        print("marking valid tiles " + start);
        visitedTiles = new HashSet<Vector3Int>();
        originalColor = GameController.getOriginalColor();
        tiles = GameManager.instance.grid;
        GameManager.ClearGrid();
        //foreach (Vector3Int tile in getValidTiles(distance, start))
        foreach (Vector3Int tile in new HashSet<Vector3Int>() { Vector3Int.left, Vector3Int.right })
        {
            print("marking " + tile);
            tiles.SetColor(tile, MoveColor);
        }
    }*/

    private HashSet<Vector3Int> getValidTiles(int depth, Vector3Int start)
    {
        start.z = 0;
        HashSet<Vector3Int> valid = new HashSet<Vector3Int>();
        if (depth > 0 && (GameManager.isTileEmpty(start) || GameManager.GetUnitAt(start) == unit))
        {
            valid.Add(start);
            print("add tile");
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Vector3Int testPos = start;
                    testPos.x += i;
                    testPos.y += j;
                    valid.UnionWith(getValidTiles(depth - 1, testPos));
                }
            }
        }
        return valid;
    }

    /*
    public override void SelectAction(bool toSet = true)
    {
        HashSet<Vector3Int> validTiles = getValidTiles(unit.GetCurrentMovement(), unit.currentPos);
        if (toSet)
        {  }
            //MarkValidTiles(unit.currentPos, unit.GetCurrentMovement(), validTiles);
        else
        {
            GameManager.ClearGrid();
        }
    }*/

}
