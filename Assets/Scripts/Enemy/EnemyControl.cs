using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class EnemyControl : MonoBehaviour
{
    //public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float health;
    public float originalSpeed;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int attackdamage = 5;
    public int hurt_damage;
    
    [SerializeField] bool playerInSightRange;
    [SerializeField] bool playerInAttackRange;

    //animation
    public Animator animator;

    public GameObject hitEffectPrefab;
    public GameObject RewardEffectPrefab;
    // public ParticleSystem hitEffect;
    public bool walk = false;
    public bool run = false;


    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool takeDamage;

    //wave
    //public SpawnEnemy scriptAReference;

    // Effect by ability
    // 2:
    public bool isSlowed = false;
    public bool slowtimer = false;
    public float slowStartTime = 0f;
    private float slowDuration = 4f;

    //attack
    public GameObject projectile;
    public GameObject rewardPrefab;

    //reward
    public float luckypoint=0.25f;
    //public float luckypoint=20;
    // Start is called before the first frame update


    public LayerMask Ground;
    [SerializeField] bool grounded;
    [SerializeField] float speed = 10;
    [SerializeField] float velLimit = 2;
    [SerializeField] float multiplier = 10;
    Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //if (!playerInSightRange && !playerInAttackRange)
        //{
        //    walk = true;
        //    run = false;
        //    Patroling();
        //}
        if (playerInSightRange && !playerInAttackRange)
        {
            Debug.Log("chase");
            walk = false;
            run = true;
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange )
        {
            Debug.Log("atk");
            AttackPlayer();
        }
        //else if (!playerInSightRange && takeDamage)
        //{
        //    walk = false;
        //    run = true;
        //    ChasePlayer();
        //}
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            transform.LookAt(player.transform);
            Vector3 vel = rb.velocity;
            grounded = Physics.Raycast(transform.position, Vector3.down, 1f, Ground);
            GetComponent<Rigidbody>().AddForce(speed * multiplier * Time.deltaTime * transform.forward);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //animator.SetFloat("Velocity", 0.2f);
        
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(player.position);
        Vector3 vel = rb.velocity;
        //if (vel.x > -velLimit && vel.x < velLimit && vel.z > -velLimit && vel.z < velLimit)
        //{
        //    //grounded = Physics.Raycast(transform.position, Vector3.down, 1f, Ground);

        //}
        GetComponent<Rigidbody>().AddForce(speed * multiplier * Time.deltaTime * transform.forward);
        if (isSlowed)
        {
            if(slowtimer)
            {
                //slowStartTime += Time.deltaTime;
                //navAgent.isStopped = true;
                //if (slowStartTime >= slowDuration)
                //{
                //    navAgent.isStopped = false;
                //    isSlowed = false;
                //    slowtimer = false;
                //    slowStartTime = 0f;
                //    slowDuration = 4f;
                //}
            }

        }
        else
        {
            
            //navAgent.SetDestination(player.position);

            //animator.SetBool("Run", run);

            //animator.SetFloat("Velocity", 0.6f);
            //navAgent.isStopped = false; // Add this line     
        }

    }


    private void AttackPlayer()
    {
        //navAgent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            //transform.LookAt(player.position);
            alreadyAttacked = true;
            animator.SetTrigger("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                /*
                    YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

                PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
                if (playerHUD != null)
                {
                   playerHUD.takeDamage(damage);
                }
                 */
            }
            if(projectile != null)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                Destroy(rb.gameObject, 4f);
            }
            

        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetTrigger("Attack");
        //animator.SetBool("Attack", false);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject + "enemy hurt");
        animator.SetTrigger("EnemyHurt");
        health -= damage;
        GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        //hitEffect.Play();
        Destroy(hitEffect, 1f);
        //StartCoroutine(TakeDamageCoroutine());
        //navAgent.enabled = true;
        if (health <= 0)
        {

            animator.SetTrigger("Die");
            //scriptAReference.score += 1;           
            // Debug.Log(scriptAReference.score);
            Die();
            //Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    public void Die()
    {
        // Perform any death-related logic here (e.g., play death animation, drop items, etc.)
        //CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        //if (capsuleCollider != null)
        //{
        //    capsuleCollider.enabled = false;
        //}
        // Destroy the enemy GameObject
        //Invoke("Reward", 2f);
        Destroy(gameObject,0.5f);
    }

    public void Reward()
    {
        GameObject player = GameObject.Find("Player");
        float maxlucky = player.GetComponent<playerscontrol>().lucky;
        float lucky = Random.Range(0f, maxlucky);
        if(lucky <= luckypoint)
        {
            GameObject RewardEffect = Instantiate(RewardEffectPrefab, transform.position, Quaternion.identity);
            //hitEffect.Play();
            Destroy(RewardEffect, 1f);
            //float lucky = UnityEngine.Random.Range(0, 100);
            if(rewardPrefab != null)
            {
                GameObject reward = Instantiate(rewardPrefab, transform.position, Quaternion.identity);
            }
                
        }
    }
        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
