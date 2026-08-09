using UnityEngine;

public class EnemyChasingState : IEnemyState
{
     private Enemy enemy;

    public EnemyChasingState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
       enemy.aipath.maxSpeed = enemy.enemyChasingSpeed; 

    }

     public void Execute()
    {
        enemy.aIDestinationSetter.target = enemy.player;
    
    }

     public void Exit()
    {
        
    }
}
