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
    //�w�t�@�Φb�ĤH���W
    void OnParticleCollision(GameObject other)
    {
        //if (!hasCollided)
        //{
            //Debug.Log("����hit");
            // �ˬd�I��������O�_�㦳�S�w������
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyControl enemy = other.GetComponent<EnemyControl>();
                // ���@�ǳB�z
                //scriptBInstance.IsSlow = true;
                enemy.isSlowed = true;
                enemy.slowtimer =true;
                //enemy.GetSlow(true);
                //Debug.Log("�I��w�t��");

                // �аO���w�gĲ�o
                //hasCollided = true;
            }
       // }
    }
}
