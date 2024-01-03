using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ability2Trigger : MonoBehaviour
{
    //private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        //if (!hasCollided)
        //{
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyControl enemy = other.GetComponent<EnemyControl>();
                enemy.isSlowed = true;
                enemy.slowtimer =true;
            }
       // }
    }
}
