using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 2.7f;
    Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        flipSprite();

        //  transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    void HandleMovement()
    {
        moveInput = GameInputManager.Instance.GetMovement();
        moveInput.Normalize();
       
    }
    
void flipSprite()
{
    if (moveInput.x > 0)
    {
        spriteRenderer.flipX = true; 
    }
    else if (moveInput.x < 0)
    {
        spriteRenderer.flipX = false; 
    }
}

}
