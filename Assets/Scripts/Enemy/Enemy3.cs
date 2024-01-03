using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class Enemy3 : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float health;
    public float originalSpeed;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int damage;
    public int hurt_damage;
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
    public float luckypoint = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        originalSpeed = navAgent.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            walk = true;
            run = false;
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            walk = false;
            run = true;
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && takeDamage)
        {
            walk = false;
            run = true;
            ChasePlayer();
        }
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
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
        if (isSlowed)
        {
            //被緩速
            if (slowtimer)
            {
                slowStartTime += Time.deltaTime;
                // 繼續緩速中
                //Debug.Log("敵人停止");
                navAgent.isStopped = true;
                if (slowStartTime >= slowDuration)
                {
                    // 緩速結束，恢復正常速度
                    Debug.Log("敵人恢復");
                    navAgent.isStopped = false;
                    isSlowed = false;
                    slowtimer = false;
                    slowStartTime = 0f;
                    slowDuration = 4f;
                }
            }

        }
        else
        {
            navAgent.SetDestination(player.position);
            animator.SetBool("Run", run);
            //animator.SetFloat("Velocity", 0.6f);
            navAgent.isStopped = false; // Add this line     
        }

    }


    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            transform.LookAt(player.position);
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
                //Debug.Log("玩家受傷");
            }
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            Debug.Log("敵人射出東西");
            Destroy(rb.gameObject, 4f);

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
        animator.SetTrigger("EnemyHurt");
        //Debug.Log("受到傷害：" + hurt_damage);
        health -= damage;
        Debug.Log("當前血量：" + health);
        GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        //hitEffect.Play();
        Destroy(hitEffect, 1f);
        //StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
        {

            animator.SetTrigger("Die");
            //scriptAReference.score += 1;           
            // Debug.Log(scriptAReference.score);
            Die();
            //Invoke(nameof(DestroyEnemy), 0.5f);
            Debug.Log("敵人死掉");
        }
    }
    void Die()
    {
        // Perform any death-related logic here (e.g., play death animation, drop items, etc.)
        //敵人死掉不要再被子彈打到
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }
        // Destroy the enemy GameObject
        Invoke("Reward", 2f);
        Destroy(gameObject, 2f);
    }

    public void Reward()
    {
        float lucky = Random.Range(0f, 1f);
        if (luckypoint <= lucky)
        {
            GameObject RewardEffect = Instantiate(RewardEffectPrefab, transform.position, Quaternion.identity);
            //hitEffect.Play();
            Destroy(RewardEffect, 1f);
            //float lucky = UnityEngine.Random.Range(0, 100);
            GameObject reward = Instantiate(rewardPrefab, transform.position, Quaternion.identity);
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
