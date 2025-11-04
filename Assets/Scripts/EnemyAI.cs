using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float attackDistance;
    public float lineSight = 15f;
    public LayerMask obstacleMask;

    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private float m_Distance;
    private bool hasLineOfSight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
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
            }


            // chase the player

            else
            {
                m_Agent.isStopped = false;
                m_Animator.SetBool("Attack", false);
                m_Agent.destination = target.position;
                Debug.DrawLine(transform.position, target.position);
            }
        //if (hasLineOfSight)
        //{ 
        //}

        //else
        //{
        //    // enemy loses sight

        //    m_Agent.isStopped = true;
        //    m_Animator.SetBool("Attack", false);
        //}
    }

    //void OnAnimatorMove() 
    //{
    //    if (m_Animator.GetBool("Attack") == false)
    //    {
    //        m_Agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
    //    }
    //}

    //bool CheckLineOfSight()
    //{
    //    Vector3 origin = transform.position + Vector3.up; // Eye height
    //    Vector3 direction = (target.position + Vector3.up - origin).normalized;

    //    if (Physics.Raycast(origin, direction, out RaycastHit hit, lineSight, ~0, QueryTriggerInteraction.Ignore))
    //    {
    //        // The ray hit something — check if it’s the target
    //        if (hit.transform == target)
    //            return true; // Clear line of sight
    //    }

    //    return false; // Blocked by an obstacle
    //}

}
