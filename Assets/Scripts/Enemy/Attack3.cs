using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3 : MonoBehaviour
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

        //Debug.Log("����hit");
        // �ˬd�I��������O�_�㦳�S�w������
        if (other.gameObject.CompareTag("Player"))
        {
            playerscontrol player = other.GetComponent<playerscontrol>();
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            // ���@�ǳB�z
            //scriptBInstance.IsSlow = true;
            //player.health -= damage;
            player.TakeDagme(damage);
            otherRigidbody.AddForce(transform.up * 20f, ForceMode.Impulse);
            //enemy.GetSlow(true);
            //Debug.Log("���a�Q�s�����j�_,�{�b��q" + player.health);
        }

    }
}
