﻿using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public float explosionDamage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Check for enemies 
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
         
            Debug.Log(enemies[i].gameObject.name);
            if (enemies[i].GetComponent<EnemySystem>() != null)
            {
                enemies[i].GetComponent<EnemySystem>().TakeDamage(explosionDamage);
            }
            else
            {

            }
            if (enemies[i].GetComponent<Enemy2>() != null)
            {
                enemies[i].GetComponent<Enemy2>().TakeDamage(explosionDamage);
            }
            else
            {

            }
            if (enemies[i].GetComponent<Enemy3>() != null)
            {
                enemies[i].GetComponent<Enemy3>().TakeDamage(explosionDamage);
            }
            else
            {

            }
            //Add explosion force (if enemy has a rigidbody)
            if (enemies[i].GetComponent<Rigidbody>())
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
                Debug.Log("explode hit" + enemies[i]);
            }

        }

        //Add a little delay, just to make sure everything works fine
        //Invoke("Delay", 0.05f);
        Destroy(gameObject);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Don't count collisions with other bullets
        if (collision.collider.CompareTag("Bullet")) return;

        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        //collision.collider.CompareTag("Enemy") && 
        if (explodeOnTouch) Explode();
    }

    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    
    //private void OnTriggerEnter(Collider other)
    //{
    //    // 如果碰撞到敌人，调用敌人的 TakeDamage 方法
    //    EnemyControl enemy = other.GetComponent<EnemyControl>();
    //    GameObject player = GameObject.Find("Player");
    //    float damage = player.GetComponent<playerscontrol>().damage;
    //    if (enemy != null)
    //    {
    //        enemy.navAgent.enabled = false;
    //        enemy.TakeDamage(damage);
           
    //        //Destroy(gameObject); // 刀击中敌人后销毁刀
    //        //Debug.Log("敵人受傷被攻擊");
    //    }
    //    /*
    //    Enemy2 enemy2 = other.GetComponent<Enemy2>();
    //    if (enemy2 != null)
    //    {
    //        enemy2.TakeDamage(damage);
    //        //Destroy(gameObject); // 刀击中敌人后销毁刀
    //        //Debug.Log("敵人2受傷被攻擊");
    //    }

    //    Enemy3 enemy3 = other.GetComponent<Enemy3>();
    //    if (enemy3 != null)
    //    {
    //        enemy3.TakeDamage(damage);
    //        //Destroy(gameObject); // 刀击中敌人后销毁刀
    //        //Debug.Log("敵人3受傷被攻擊");
    //    }
    //    */
    //}

  
}
