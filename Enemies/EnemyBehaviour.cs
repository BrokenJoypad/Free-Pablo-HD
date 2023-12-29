using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float timeBeforeSelectingNewTarget = 2f;

    private bool MovingTowardsTarget = false;
    [SerializeField] private GameObject targetToMoveTowards;



    void Start()
    {
        checkAreaCollider = GetComponentInChildren<CheckAreaCollider>();
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();
        MaxSearchRange = stats.ReturnBaseAttackRange();
        MoveSpeed = stats.ReturnBaseMovementSpeed();
    }

    void Update()
    {
        searchTimer += Time.deltaTime;
        targetToMoveTowards = checkAreaCollider.ReturnTargetsInArea();

        CheckCanMove();
        Patrol();
        MoveTowardsTarget();

    }

    void MoveTowardsTarget()
    {

        if (targetToMoveTowards != null)
        {
            Vector3 targetPosition = targetToMoveTowards.transform.position;
            targetPosition.y = rb.transform.position.y;
            rb.transform.LookAt(targetPosition);

            // Calculate the movement direction and then move towards the target
            Vector3 moveDirection = (targetPosition - rb.transform.position).normalized;
            rb.velocity = moveDirection * MoveSpeed;
            Debug.DrawLine(rb.transform.position, targetPosition, Color.red);
        }
        else
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.05f * Time.deltaTime);
            checkMovingTowardsTarget();
    }

    void Patrol()
    {
        if (checkAreaCollider.ReturnTargetsInArea() == null)
        {
            if (PatrolTarget == Vector3.zero)
            {
                searchForNewPatrolTarget();
            }
            else
            {
                    moveToPatrolTarget();
                    Debug.DrawLine(rb.transform.position, PatrolTarget, Color.blue);
            }

        }
        else
        {
            PatrolTarget = Vector3.zero;
            searchTimer = 0f;
            searchingForNewTarget = false;
        }
    }

    void CheckCanMove()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out RaycastHit hit, 2f))
        {
            if (hit.rigidbody != null || hit.collider != null)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
        else
        {
            canMove = true;
        }
    }

    void searchForNewPatrolTarget()
    {
        if (searchTimer <= timeBeforeSelectingNewTarget)
        {
            searchingForNewTarget = true;
            PatrolTarget = Vector3.zero;
        }
        else
        {
            Vector2 randomCircle = Random.insideUnitCircle * MaxSearchRange;
            PatrolTarget = new Vector3(randomCircle.x + rb.transform.position.x, rb.transform.position.y, randomCircle.y + rb.transform.position.z);
            searchingForNewTarget = false;
            searchTimer = 0f;
        }
    }

    void moveToPatrolTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(PatrolTarget - rb.transform.position);
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRotation, Time.deltaTime * 3f);
        Vector3 distanceBetween = PatrolTarget - rb.transform.position;
        if (distanceBetween.magnitude > 1f)
        {
            rb.velocity = distanceBetween.normalized * MoveSpeed;
            MovingToPatrolTarget = true;
        }
        else
        {
            PatrolTarget = Vector3.zero;
            searchTimer = 0;
            MovingToPatrolTarget = false;
        }
    }

    void checkMovingTowardsTarget()
    {
        if (rb.velocity == Vector3.zero)
        {
            MovingTowardsTarget = false;
        }
        else
        {
            MovingTowardsTarget = true;
        }
    }

    public bool returnSearchState()
    {
        return searchingForNewTarget;
    }

    public bool returnMoveState()
    {
        return MovingTowardsTarget;
    }



}
