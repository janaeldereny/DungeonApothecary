using UnityEngine;
using System.Collections;

public class EnemyCalmingState : IEnemyState
{
    private Enemy enemy;
    public EnemyCalmingState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        enemy.floatingIcon.SetActive(false);

        enemy.spriteRenderer.color = Color.white;

        enemy.aipath.canMove = false;

        enemy.rb.linearVelocity = Vector2.zero;
        enemy.rb.angularVelocity = 0f;
        enemy.rb.bodyType = RigidbodyType2D.Kinematic;

        enemy.StartCoroutine(CalmingRoutine());
    }

     public void Execute()
    {
    }

     public void Exit()
    {
    }

    private IEnumerator CalmingRoutine()

    {

        yield return new WaitForSeconds(enemy.calmingDuration);

        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
        enemy.aipath.canMove = true;

        enemy.ChangeState(new EnemyExitingState(enemy));
    }
}
