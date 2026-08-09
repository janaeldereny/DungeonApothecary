using UnityEngine;
public class EnemyWaitingState : IEnemyState
{
    private Enemy enemy;

    public EnemyWaitingState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        enemy.floatingIcon.SetActive(true);
    }

     public void Execute()
    {
        enemy.patienceTimer -= Time.deltaTime;
        if (enemy.patienceTimer <= 0f)

        {
            enemy.ChangeState(new EnemyChasingState(enemy));
        }

    
    }

     public void Exit()
    {
        
    }
}
