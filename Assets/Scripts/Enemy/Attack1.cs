using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Attack1 : MonoBehaviour
{
    
    public float damage;
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
        if (other.gameObject.CompareTag("Player"))
        {
            playerscontrol player = other.GetComponent<playerscontrol>();
            //scriptBInstance.IsSlow = true;
            //player.health -= damage;
            Debug.Log("player-HP");
            player.TakeDagme(damage);
            //enemy.GetSlow(true);
        }
        
    }
}
