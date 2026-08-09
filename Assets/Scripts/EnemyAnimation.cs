using UnityEngine;
using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    private AIPath aiPath;

    [SerializeField] private Animator animator;


    private void Awake()

    {

        aiPath = GetComponent<AIPath>();

    }



    private void Update()

    {

        Vector2 velocity = aiPath.velocity;
        bool isWalking = velocity.sqrMagnitude > 0.01f;
         animator.SetBool("isWalking", isWalking);
        if (isWalking)

        {

            Vector2 direction = velocity.normalized;

            animator.SetFloat("moveX", direction.x);

            animator.SetFloat("moveY", direction.y);

            animator.SetFloat("lastmoveX", direction.x);

            animator.SetFloat("lastmoveY", direction.y);

        }

    }

    public void SetIdleDirection(bool facingRight)
{
    animator.SetFloat("lastmoveX", facingRight ? 1 : -1);
    animator.SetFloat("lastmoveY", 0);
}
    


}
