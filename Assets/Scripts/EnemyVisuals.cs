using UnityEngine;
using Pathfinding;

public class MonsterVisuals : MonoBehaviour
{
    private AIPath aiPath;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private void Awake()

    {

        aiPath = GetComponent<AIPath>();

        animator = GetComponentInChildren<Animator>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    private void Update()

    {

        float speed = aiPath.velocity.magnitude;

        animator.SetFloat("Speed", speed);

        if (aiPath.velocity.x > 0.05f)

            spriteRenderer.flipX = false;

        else if (aiPath.velocity.x < -0.05f)

            spriteRenderer.flipX = true;

    }


}

