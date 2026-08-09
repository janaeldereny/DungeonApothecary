using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;  
    public Transform player;
    public AIDestinationSetter aIDestinationSetter;
    public AIPath aipath;
    public Transform doorC;
    
     public float calmingDuration = 1f;
     public float enemyExitingSpeed = 5f;
     public float enemyChasingSpeed = 1.5f;
     public float patienceTimer = 4f;

     //public EnemyStates currentState;

     public SpriteRenderer spriteRenderer;
     public GameObject floatingIcon;

     private IEnemyState currentStat;


    public event Action<Enemy> OnEnemyExited;

        void Start()
        {
            floatingIcon.SetActive(true);
            ChangeState(new EnemyWaitingState(this));
            aipath = GetComponent<AIPath>();
        }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        doorC = GameObject.FindGameObjectWithTag("DoorC").transform;

    }

    
    void Update()
    {
        currentStat?.Execute();
    }

    public void ChangeState(IEnemyState newState)
    {
        currentStat?.Exit();

        currentStat = newState;

        currentStat.Enter();
    }

    public bool CanBeCured()
    {
        return currentStat is EnemyWaitingState || currentStat is EnemyChasingState;
    }

    public void Calm()
    {
        ChangeState(new EnemyCalmingState(this));
    }

    public void EnemyExited()
    {
        OnEnemyExited?.Invoke(this);
        Destroy(gameObject);

    }

public void SetPatienceTimer(float value)
{
    patienceTimer = value;
}


}
