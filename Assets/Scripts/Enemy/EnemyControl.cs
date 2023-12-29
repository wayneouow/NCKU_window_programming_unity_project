using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
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
            //�Q�w�t
            if(slowtimer)
            {
                slowStartTime += Time.deltaTime;
                // �~��w�t��
                //Debug.Log("�ĤH����");
                navAgent.isStopped = true;
                if (slowStartTime >= slowDuration)
                {
                    // �w�t�����A��_���`�t��
                    Debug.Log("�ĤH��_");
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
                Debug.Log("���a����");
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
        animator.SetTrigger("EnemyHurt");
        Debug.Log("����ˮ`�G" + hurt_damage);
        health -= damage;
        Debug.Log("��e��q�G" + health);
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
            Debug.Log("�ĤH����");
        }
    }
    void Die()
    {
        // Perform any death-related logic here (e.g., play death animation, drop items, etc.)
        
        // Destroy the enemy GameObject
        Destroy(gameObject,2f);
    }
    /*
    private IEnumerator TakeDamageCoroutine()
    {
        takeDamage = true;
        yield return new WaitForSeconds(1f);
        takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine());
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        //animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
