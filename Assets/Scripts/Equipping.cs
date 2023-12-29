using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipping : MonoBehaviour
{
    public GameObject[] Slots;
    public int maxSlotNum;
    public bool isFulled;
    public GameObject slotForThrowing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slots[0] = slotForThrowing;
    }
    void getItem(GameObject item)
    {

    }
    
}
