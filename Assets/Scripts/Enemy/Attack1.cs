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

    //受傷(被球丟到 被火燙到)
    void OnParticleCollision(GameObject other)
    {
        
        //Debug.Log("測試hit");
        // 檢查碰撞的物件是否具有特定的標籤
        if (other.gameObject.CompareTag("Player"))
        {
            playerscontrol player = other.GetComponent<playerscontrol>();
            // 做一些處理
            //scriptBInstance.IsSlow = true;
            //player.health -= damage;
            player.TakeDagme(damage);
            //enemy.GetSlow(true);
            //Debug.Log("玩家受傷,現在血量" + player.health);   
        }
        
    }
}
