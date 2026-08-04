using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;

    public AudioSource audioSource;
    public AudioClip[] footstepSounds ;
    [SerializeField] private float footstepInterval = 0.5f;
    [SerializeField] private bool isPlayingFootstepSound = false;
    
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
