using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 50;
    private float attackCooldown = 1f;
    private float attackTimer = 0f;

    public Transform target;
    public float attackDistance;
    public float lineSight = 15f;
    public LayerMask obstacleMask;

    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private float m_Distance;
    private bool hasLineOfSight;
    private PlayerController characters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }

        characters = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        m_Distance = Vector3.Distance(m_Agent.transform.position, target.position);
        //hasLineOfSight = CheckLineOfSight();

            // in attack range 

            if (m_Distance < attackDistance )
            {
                m_Agent.isStopped = true;
                m_Animator.SetBool("Attack", true);

                DamagePlayer();
            }


            // chase the player

            else
            {
                m_Agent.isStopped = false;
                m_Animator.SetBool("Attack", false);
                m_Agent.destination = target.position;
                Debug.DrawLine(transform.position, target.position);
            }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        m_Agent.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;

        Destroy(gameObject, 2f);
    }

    public void DamagePlayer()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            return;
        }

        attackTimer = attackCooldown;

        characters.TakeDamage(damage);
        Debug.Log("Player took damage");
    }
}
