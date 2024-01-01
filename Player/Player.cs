using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField, Range(0f, 500f)] private float BaseHealth;
    [SerializeField, Range(0f, 10f)] private float BaseMovementSpeed;
    [SerializeField, Range(0f, 500f)] private float BaseAttackDamage;
    [SerializeField, Range(0f, 100f)] private float BaseAttackRange;

    [Header("Stat Bonus %")]
    [SerializeField, Range(0f, 100f)] private float BonusHealth;
    [SerializeField, Range(0f, 100f)] private float BonusMovementSpeed;
    [SerializeField, Range(0f, 100f)] private float BonusAttackDamage;
    [SerializeField, Range(0f, 100f)] private float BonusAttackRange;

    [Header("Stats")]
    [SerializeField, Range(0f, 500f)] private float Health;
    [SerializeField, Range(0f, 10f)] private float MovementSpeed;
    [SerializeField, Range(0f, 500f)] private float AttackDamage;
    [SerializeField, Range(0f, 100f)] private float AttackRange;

    [Header("Player State")]
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isAttackingTarget = false;
    [SerializeField] private bool ArrivedAtClickedTarget = false;


    public static Player Instance;
    private float StoppingDistance;
    private NavMeshAgent navMeshAgent;
    private Vector3 ClickedTargetPosition;
    private GameObject ClickedTargetGameObject;
    private Rigidbody rb;
    private GameCursor gameCursor;


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameCursor = GameCursor.Instance;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {   
        
        if (Input.GetButton("LClick"))
        {
            ClickedTargetPosition = gameCursor.ReturnCursorPosition();
            ClickedTargetGameObject = gameCursor.GetHitGameObject();
        }

        MoveToClickedTarget();

        if (ClickedTargetGameObject != null && ArrivedAtClickedTarget)
        {
            IAttackable attackable = ClickedTargetGameObject.GetComponent<IAttackable>();

            if (attackable != null)
            {
                Attack();
                attackable.TakeDamage();
            }
        }
        else
        {
            isAttackingTarget = false;
        }

    }


    void MoveToClickedTarget()
    {
        navMeshAgent.destination = ClickedTargetPosition;
        rb.transform.LookAt(navMeshAgent.steeringTarget);

        if (ClickedTargetGameObject != null && ClickedTargetGameObject.name == "Environment__Ground")
        {
            StoppingDistance = 0.1f;
        }
        else
        {
            StoppingDistance = 2f;
        }


        if (navMeshAgent.remainingDistance <= StoppingDistance)
        {
            ArrivedAtClickedTarget = true;
            isMoving = false;
        }
        else
        {
            ArrivedAtClickedTarget = false;
            isMoving = true;
        }



    }

    void Attack()
    {
        isAttackingTarget = true;
    }

    public bool ReturnPlayerAttackState()
    {
        return isAttackingTarget;
    }

    public bool ReturnPlayerMoveState()
    {
        return isMoving;
    }

}
