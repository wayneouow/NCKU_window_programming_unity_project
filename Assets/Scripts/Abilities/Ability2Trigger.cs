using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ability2Trigger : MonoBehaviour
{
    //private bool hasCollided = false;
    // Start is called before the first frame update
    bool isInRange; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("SLOW");
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemySystem enemy = other.GetComponent<EnemySystem>();
            enemy.enfreeze();
        }


    }

    
}
