using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ability3Trigger : MonoBehaviour
{
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
            // ���@�ǳB�z
            //scriptBInstance.IsSlow = true;
            player.isHealed = true;
            player.healtimer = true;
            //enemy.GetSlow(true);
            //Debug.Log("�I��v¡��");

            // �аO���w�gĲ�o
            //hasCollided = true;
        }
    }
}
