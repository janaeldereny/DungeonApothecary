using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 2.7f;
    Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //flipSprite();

        //  transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;
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
            //animator.SetBool("isWalking" , false);
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
    
// void flipSprite()
// {
//     if (moveInput.x > 0)
//     {
//         spriteRenderer.flipX = true; 
//     }
//     else if (moveInput.x < 0)
//     {
//         spriteRenderer.flipX = false; 
//     }
// }

}
