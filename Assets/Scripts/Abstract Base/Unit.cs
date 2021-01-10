using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    private int food;
    [SerializeField]
    private int gold;
    [SerializeField]
    private int stone;
    [SerializeField]
    private int wood;
    
    [SerializeField]
    private bool canMove = true;

    public int maxMovement;
    [SerializeField]
    private int currentMovement;

    public List<Action> actions;
    private int currentAction = 0;

    public Vector3Int currentPos;

    public GameObject ButtonsHolder;

    private void Start()
    {
        currentPos = Vector3Int.FloorToInt(transform.position);
        currentPos.z = 0;
        ReloadAction();
        for (int i = 0; i < ButtonsHolder.transform.childCount; i++)
            ButtonsHolder.transform.GetChild(i).GetComponent<Image>().color = GameManager.SetAlpha(Color.grey, 100);
        ButtonsHolder.transform.GetChild(currentAction).GetComponent<Image>().color = Color.white;
    }

    public bool isValidTile(Vector3Int pos)
    {
        return false;
    }

    public void MoveUnit(Vector3Int newPos)
    {
        if (currentPos != newPos)
        {
            GameManager.MoveUnit(newPos, this);
            transform.position = newPos;
            currentPos = newPos;
        }
    }

    public void NextAction()
    {
        actions[currentAction].SelectAction(false);
        ButtonsHolder.transform.GetChild(currentAction).GetComponent<Image>().color = GameManager.SetAlpha(Color.grey, 100);
        currentAction++;
        if (currentAction >= actions.Count)
            currentAction = 0;
        ButtonsHolder.transform.GetChild(currentAction).GetComponent<Image>().color = Color.white;
        actions[currentAction].SelectAction();
    }

    public void ReloadAction()
    {
        actions[currentAction].SelectAction();
    }

    

    //getters
    public int GetFood() { return food; }
    public int GetGold() { return gold; }
    public int GetStone() { return stone; }
    public int GetWood() { return wood; }
    public int GetCurrentMovement() { return currentMovement; }

    //like .pop() in python, these functions take away from the resource
    //and return the amount taken to use
    public int TakeFood(int count)
    {
        food -= count;
        return count;
    }
    public int TakeGold(int count)
    {
        gold -= count;
        return count;
    }
    public int TakeStone(int count)
    {
        stone -= count;
        return count;
    }
    public int TakeWood(int count)
    {
        wood -= count;
        return count;
    }
    public void AddFood(int amount) { food += amount; }
    public void AddGold(int amount) { gold += amount; }
    public void AddStone(int amount) { stone += amount; }
    public void AddWood(int amount) { wood += amount; }
}
