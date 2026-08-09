using UnityEngine;
using System;

public class EnemyExitingState  : IEnemyState
{
   
     private Enemy enemy;
     public event Action<Enemy> OnEnemyExited;

    public EnemyExitingState (Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        enemy.aIDestinationSetter.target = enemy.doorC;
        enemy.aipath.maxSpeed = enemy.enemyExitingSpeed; 
        NotifyEnemyExited();
       
    }

     public void Execute()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.doorC.position) < 0.2f)

        {

            enemy.EnemyExited();

        }

    }
     public void Exit()
    {
       
    }

    public void NotifyEnemyExited()
    {
        OnEnemyExited?.Invoke(enemy);
    }
}
