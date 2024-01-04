using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private CheckAreaCollider checkAreaCollider;
    private EnemyStats stats;

    private bool canMove = false;
    public bool MovingToPatrolTarget = false;
    public bool searchingForNewTarget = false;
    public Vector3 PatrolTarget;

    private float MaxSearchRange;
    private float MoveSpeed;
    private float searchTimer = 0f;
    private float NoTargetFoundTimer = 0f; // used for checking how many seconds it's been since we last found anything in the area collider.
    private float timeBeforeSelectingNewTarget = 3f;
    private NavMeshAgent navMeshAgent;
    private bool canStartPatrol = true;

    private bool MovingTowardsTarget = false;
    [SerializeField] private GameObject targetToMoveTowards;

    // check if we have a target in our area
    // if we do, store it's position and move towards it.
    // if targets in area has been null for more than let's say 3 seconds..
    // pick a random target within our search range
    // move towards it.

    // Start null timer
    // if nulltimer == 3f and targets in area == null
    // PATROL 
        //  select a random target in area
        //  COROUTINE? wait 2s, set is searching to true
        //  rotate towards the patrol target
        //  move towards it
        //  when arrived, set null timer to 0.
    // else if targets in area != null 
    // MOVE TOWARDS TARGET
        // set navmesh position to the target in area.

    void Start()
    {
        checkAreaCollider = GetComponentInChildren<CheckAreaCollider>();
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();
        MaxSearchRange = stats.ReturnBaseAttackRange();
        MoveSpeed = stats.ReturnBaseMovementSpeed();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        searchTimer += Time.deltaTime;
        NoTargetFoundTimer += Time.deltaTime;
        targetToMoveTowards = checkAreaCollider.ReturnTargetsInArea();

        if (canStartPatrol)
        {
            Patrol();
        }

        if(targetToMoveTowards != null)
        {
            canStartPatrol = false;
            navMeshAgent.destination = targetToMoveTowards.transform.position;
        }


    }

    void Patrol()
    {
        if (!searchingForNewTarget && navMeshAgent.remainingDistance == 0f && canStartPatrol)
        {
            StartCoroutine(GetNewPatrolTarget());
            searchingForNewTarget = true;
            canStartPatrol = false;
        }
        else
        {
            navMeshAgent.destination = PatrolTarget;
            searchingForNewTarget = false;
            searchTimer = 0;
            canStartPatrol = true;
        }

    }

    IEnumerator GetNewPatrolTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * MaxSearchRange;
        PatrolTarget = new Vector3(randomCircle.x + rb.transform.position.x, rb.transform.position.y, randomCircle.y + rb.transform.position.z);
        searchingForNewTarget = true;
        yield return new WaitForSeconds(timeBeforeSelectingNewTarget);
        canStartPatrol = true;
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
