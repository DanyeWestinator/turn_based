using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Unit
{
    public static void CreateCrate(Vector3Int pos, int food, int gold, int wood, int stone)
    {
        pos.z = -2;
        GameObject cratePrefab = Resources.Load<GameObject>("CratePrefab");
        GameObject go = Instantiate(cratePrefab);
        go.transform.position = pos;
        Unit unit = go.GetComponent<Unit>();
        unit.AddFood(food);
        if (food == 0)
            go.transform.GetChild(0).Find("foodIcon").gameObject.SetActive(false);
        unit.AddGold(gold);
        if (gold == 0)
            go.transform.GetChild(0).Find("goldIcon").gameObject.SetActive(false);
        unit.AddStone(stone);
        if (stone == 0)
            go.transform.GetChild(0).Find("stoneIcon").gameObject.SetActive(false);
        unit.AddWood(wood);
        if (wood == 0)
            go.transform.GetChild(0).Find("woodIcon").gameObject.SetActive(false);
    }
}
