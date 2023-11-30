
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAILogic : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    public float health;

    private float attackCooldown = 1.5f;
    private float currentCooldown = 0.0f;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public BoxCollider armCollider;
    public BoxCollider legCollider;

    private bool canMove = false;

    private Animator animator;

    public Transform headTransform;
    public Transform leftArmTransform;
    public Transform rightArmTransform;
    public Transform upperChestTransform;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        armCollider = GetComponentInChildren<BoxCollider>();
        legCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(StartMovingAfterDelay(2f));
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange && canMove) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void ChasePlayer()
    {   
        if (agent.isOnNavMesh)
        {
            animator.SetInteger("RunningIndex", Random.Range(0, 5));
            agent.SetDestination(player.position); 
        }
        
    }

    private void AttackPlayer()
    {
        
        if (currentCooldown <= 0)
        {
            animator.SetInteger("AttackIndex", Random.Range(0, 2));
            animator.SetTrigger("Attack");
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            currentCooldown = attackCooldown;
        }

        transform.LookAt(player);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void EnableAttack()
    {
        armCollider.enabled = true;
        legCollider.enabled = true;
    }

    private void DisableAttack()
    {
        armCollider.enabled = false;
        legCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.CompareTag("Player");
        if (armCollider.enabled || legCollider.enabled)
        {
            Debug.Log("HIT");
        
            PlayerStats.Instance.TakeDamage(1);
            UIManager.Instance.UpdateHealthUI();
        }
    }

    // Coroutine to start moving after a delay
    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true; // Allow the AI to move
    }

    public Vector3 HeadPosition
    {
        get
        {
            return headTransform.position;
        }
    }

    public Vector3 LeftArmPosition
    {
        get
        {
            return leftArmTransform.position;
        }
    }

    public Vector3 RightArmPosition
    {
        get
        {
            return rightArmTransform.position;
        }
    }

    public Vector3 UpperChestPosition
    {
        get
        {
            return upperChestTransform.position;
        }
    }
}
