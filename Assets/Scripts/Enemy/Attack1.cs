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

    //����(�Q�y��� �Q���S��)
    void OnParticleCollision(GameObject other)
    {
        
        //Debug.Log("����hit");
        // �ˬd�I��������O�_�㦳�S�w������
        if (other.gameObject.CompareTag("Player"))
        {
            playerscontrol player = other.GetComponent<playerscontrol>();
            // ���@�ǳB�z
            //scriptBInstance.IsSlow = true;
            //player.health -= damage;
            player.TakeDagme(damage);
            //enemy.GetSlow(true);
            //Debug.Log("���a����,�{�b��q" + player.health);   
        }
        
    }
}
