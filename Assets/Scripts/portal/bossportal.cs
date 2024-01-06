using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bossportal : MonoBehaviour
{
    public GameObject myObject;
    //public string scenename;
 

    
    // Start is called before the first frame update
    void Start()
    {
        myObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject boss = GameObject.Find("Phantom _BOSS(Clone)");
        if(boss != null)
        {
            Debug.Log("Back portal GO!");
            if(boss.GetComponent<TornatoEnemy>().HP <= 0f)
            {
                myObject.SetActive(true);
            }
        }
       
        
    }
}
