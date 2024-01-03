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
    //緩速作用在敵人身上
    void OnParticleCollision(GameObject other)
    {
        //if (!hasCollided)
        //{
            //Debug.Log("測試hit");
            // 檢查碰撞的物件是否具有特定的標籤
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyControl enemy = other.GetComponent<EnemyControl>();
                // 做一些處理
                //scriptBInstance.IsSlow = true;
                enemy.isSlowed = true;
                enemy.slowtimer =true;
                //enemy.GetSlow(true);
                //Debug.Log("碰到緩速圈");

                // 標記為已經觸發
                //hasCollided = true;
            }
       // }
    }
}
