using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private CheckAreaCollider checkAreaCollider;
    private EnemyStats stats;

    [SerializeField] private bool canStartPatrol = true;
    [SerializeField] private bool MovingToPatrolTarget = false;
    [SerializeField] private bool searchingForNewTarget = false;
    public Vector3 PatrolTarget;

    private float MaxSearchRange;
    private float timeBeforeSelectingNewTarget = 3f;
    private NavMeshAgent navMeshAgent;
    

    private bool MovingTowardsTarget = false;
    [SerializeField] private GameObject targetToMoveTowards;



    void Start()
    {
        checkAreaCollider = GetComponentInChildren<CheckAreaCollider>();
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();
        MaxSearchRange = stats.ReturnBaseAttackRange();
        navMeshAgent = GetComponent<NavMeshAgent>();
        PatrolTarget = rb.transform.position;
    }

    void Update()
    {
        targetToMoveTowards = checkAreaCollider.ReturnTargetsInArea();


        if (targetToMoveTowards)
        {
            navMeshAgent.destination = targetToMoveTowards.transform.position;

        } else{
                Patrol();
        }


    }

    void Patrol()
    {
        if (!searchingForNewTarget && navMeshAgent.remainingDistance == 0f)
        {
            searchingForNewTarget = true;
            StartCoroutine(GetNewPatrolTarget());
        } else
        {
            navMeshAgent.destination = PatrolTarget;
            searchingForNewTarget = false;
            canStartPatrol = false;         
            MovingTowardsTarget = true;
        }

    }

    IEnumerator GetNewPatrolTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * MaxSearchRange;
        PatrolTarget = new Vector3(randomCircle.x + rb.transform.position.x, rb.transform.position.y, randomCircle.y + rb.transform.position.z);
        yield return new WaitForSeconds(timeBeforeSelectingNewTarget);
        searchingForNewTarget = false;
    }

    public bool returnSearchState()
    {
        return searchingForNewTarget;
    }

    public bool returnMoveState()
    {
        return MovingTowardsTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(PatrolTarget, 0.4f);
    }
}
