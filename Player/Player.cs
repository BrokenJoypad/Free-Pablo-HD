using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isAttackingTarget = false;
    [SerializeField] private bool ArrivedAtClickedTarget = false;


    public static Player Instance;
    private float StoppingDistance;
    private Vector3 ClickedTargetPosition;
    private GameObject ClickedTargetGameObject;
    private Rigidbody rb;
    private GameCursor gameCursor;


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameCursor = GameCursor.Instance;

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
        CheckCanMove();
        
        if (Input.GetButton("LClick"))
        {
            ClickedTargetPosition = gameCursor.ReturnCursorPosition();
            ClickedTargetGameObject = gameCursor.GetHitGameObject();
            rb.transform.LookAt(ClickedTargetPosition);
        }
    }

    void FixedUpdate()
    {
        MoveToClickedTarget();

        if (ClickedTargetGameObject != null && ArrivedAtClickedTarget){
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
        Vector3 distanceBetween = ClickedTargetPosition - rb.position;

        if(ClickedTargetGameObject != null && ClickedTargetGameObject.name == "Environment__Ground")
        {
            StoppingDistance = 0.1f;
        }
        else
        {
            StoppingDistance = 2f;
        }

        if (canMove && distanceBetween.magnitude > StoppingDistance)
        {
            Vector3 movementDirection = distanceBetween.normalized;
            Vector3 movement = movementDirection * MovementSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
            isMoving = true;
            ArrivedAtClickedTarget = false;

        } else if(distanceBetween.magnitude < StoppingDistance) {
            ArrivedAtClickedTarget = true;
            isMoving = false;
            rb.velocity = Vector3.zero;
        }

    }

    void CheckCanMove(){
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out RaycastHit hit, 1f)) {
            if (hit.rigidbody != null || hit.collider != null) {
                canMove = false;
                isMoving = false;
            } else {
                canMove = true;
            }
        } else {
            canMove = true;
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
