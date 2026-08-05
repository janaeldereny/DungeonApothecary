using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        HandleMovement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        bool isMoving = moveInput != Vector2.zero;

        animator.SetBool("isWalking",isMoving);

        if (isMoving)
        {
             animator.SetFloat("InputX" , moveInput.x );
            animator.SetFloat("InputY" , moveInput.y );
            
            animator.SetFloat("LastInputX" , moveInput.x );
            animator.SetFloat("LastInputY" , moveInput.y );
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    void HandleMovement()
    {
        moveInput = GameInputManager.Instance.GetMovement();
        moveInput.Normalize();
       
    }

   

}
