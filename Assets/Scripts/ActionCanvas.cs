using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour
{
    public float xPos = 0f;
    public RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rt.anchoredPosition = new Vector3(xPos, rt.anchoredPosition.y, 0f);
    }

    //private float GetWidth
}
