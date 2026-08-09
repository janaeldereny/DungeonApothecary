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

        enemy.aipath.canMove = true;

        enemy.ChangeState(new EnemyExitingState(enemy));
    }
}
