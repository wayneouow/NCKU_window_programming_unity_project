using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    //public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject canvasObject = GameObject.FindWithTag("rewardtag");
            Canvas canvas = canvasObject.GetComponent<Canvas>();
            canvas.enabled = true;
            //Debug.Log(canvas.gameObject.name);
            //Debug.Log(canvas.enabled);

        }
    }
}
