using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private CheckAreaCollider checkAreaCollider;
    private EnemyStats stats;

    public bool MovingToPatrolTarget = false;
    public bool searchingForNewTarget = false;
    public Vector3 PatrolTarget;

    private float MaxSearchRange;
    private float MoveSpeed;
    private float searchTimer = 0f;
    private float timeBeforeSelectingNewTarget = 2f;

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

        } else{
            PatrolTarget = Vector3.zero;
            searchTimer = 0f;
            searchingForNewTarget = false;
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

    public bool returnSearchState(){
        return searchingForNewTarget;
    }
}
