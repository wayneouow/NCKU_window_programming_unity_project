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

        //Debug.Log("測試hit");
        // 檢查碰撞的物件是否具有特定的標籤
        if (other.gameObject.CompareTag("Player"))
        {
            playerscontrol player = other.GetComponent<playerscontrol>();
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            // 做一些處理
            //scriptBInstance.IsSlow = true;
            //player.health -= damage;
            player.TakeDagme(damage);
            otherRigidbody.AddForce(transform.up * 20f, ForceMode.Impulse);
            //enemy.GetSlow(true);
            //Debug.Log("玩家被龍捲風吹起,現在血量" + player.health);
        }

    }
}
