using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    public bool found = false;
    public bool lookat = false;
    public bool near = false;

    public GameObject player;
    public LayerMask playerLayer;
    public bool isMoving;
    public float speed = 10;

    public static float slowRate = 1;

    [SerializeField] float velLimit = 2;
    [SerializeField] float multiplier = 10;
    public LayerMask Ground;
    [SerializeField] bool grounded;
    [SerializeField] float jumpForce = 1;

    public float sightRange;
    public float attackRange;

    public GameObject hitEffectPrefab;

    [Header("Health")]
    public float MaxHP = 100;
    public float HP = 100;
    public bool alive = true;
    public GameObject Drop;
    public GameObject DropEffect;
    bool dropped = false;
    [Header("Attack")]
    public bool canAttack = true;
    public float AttackCoolDown = 0.7f;
    public bool isAttacking = false;
    public float atk = 10;
    public GameObject atkParticle;

    [Header("Sound")]
    public AudioClip slimeSound;

    Rigidbody rb;
    Animator animator;
    public float luckypoint = 0.25f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animatorSet();
        if (alive)
        {
            found = Physics.CheckSphere(transform.position, sightRange, playerLayer);
            near = Physics.CheckSphere(transform.position, attackRange, playerLayer);
            if (found)
            {
                //Debug.Log("Found");
                transform.LookAt(player.transform);
                Vector3 vel = rb.velocity;
                rb.useGravity = false;
                if (near)
                {

                    GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    GetComponent<Rigidbody>().AddForce(slowRate * 0.8f * speed * multiplier * Time.deltaTime * transform.forward);

                    //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                }
                if (!near && vel.x > -velLimit && vel.x < velLimit && vel.z > -velLimit && vel.z < velLimit)
                {
                    grounded = Physics.Raycast(transform.position, Vector3.down, 1f, Ground);
                    GetComponent<Rigidbody>().AddForce(slowRate * speed * multiplier * Time.deltaTime * transform.forward);

                    if (grounded)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                        rb.AddForce(slowRate * transform.up * jumpForce, ForceMode.Impulse);
                    }
                }

            }
            else
            {
                rb.useGravity = true;
            }
            if (rb.velocity.magnitude > 0.2f)
            {
                isMoving = true;
            }
            else
                isMoving = false;

            if (canAttack && near)
            {
                isAttacking = true;
                canAttack = false;
                Attack();
                Invoke(nameof(AttackReset), AttackCoolDown);
            }


        }
        else
        {

            //transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y*0.3f, transform.localScale.z);
            //
            Destroy(gameObject);
            if (!dropped)
            {
                dropped = true;
                if(DropEffect != null)
                    Instantiate(DropEffect, new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y, transform.position.z + Random.Range(-3f, 3f)), transform.rotation);
                if (Drop != null)
                {
                    //Instantiate(Drop, new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y, transform.position.z + Random.Range(-3f, 3f)), transform.rotation);
                    Invoke("Reward", 2f);
                    Destroy(gameObject, 2f);
                }
            }
            
        }
        
    }
    public void Reward()
    {
        GameObject player = GameObject.Find("Player");
        float maxlucky = player.GetComponent<playerscontrol>().lucky;
        float lucky = Random.Range(0f, maxlucky);
        //Debug.Log("隨機幸運"+lucky +"vs 敵人幸運" +luckypoint);
        if (lucky <= luckypoint)
        {
            //GameObject RewardEffect = Instantiate(RewardEffectPrefab, transform.position, Quaternion.identity);
            //hitEffect.Play();
            //Destroy(RewardEffect, 1f);
            //float lucky = UnityEngine.Random.Range(0, 100);
            GameObject reward = Instantiate(Drop, transform.position, Quaternion.identity);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        if(GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>()?.Play();
        if(atkParticle != null)
        {
            Instantiate(atkParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        }
        //player.GetComponent<PlayerValue>().HP -= atk;
    }
    private void AttackReset()
    {
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        GameObject player = GameObject.Find("Player");
        float attackrate = player.GetComponent<playerscontrol>().attackrate;
        Debug.Log(gameObject + "enemy hurt" + HP.ToString());
        animator.SetTrigger("EnemyHurt");
        HP -= (damage* attackrate);
        if(hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 1f);
        }
            
        if (HP <= 0)
        {
            animator.SetTrigger("Die");
            alive = false;
        }
    }

    private void animatorSet()
    {
        animator.SetBool("Walk", isMoving);
        //animator.SetBool("dead", !alive);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void enfreeze()
    {
        slowRate = 0f;
        Invoke("enResetspeed", 4f);
    }


    public void enResetspeed()
    {
        slowRate = 1f;
    }
}
