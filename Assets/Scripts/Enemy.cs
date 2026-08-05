using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;  
    private Transform player;
    [SerializeField]private AIDestinationSetter aIDestinationSetter;
    [SerializeField] private AIPath aipath;
    [SerializeField] private Transform doorC;
    
     [SerializeField] private float calmingDuration = 1f;
     [SerializeField] private float enemyExitingSpeed = 5f;
     [SerializeField] private float enemyChasingSpeed = 1.5f;
     [SerializeField] private float patienceTimer = 4f;

     public EnemyStates currentState;

     [SerializeField] private SpriteRenderer spriteRenderer;
     [SerializeField] private GameObject floatingIcon;


    public event Action<Enemy> OnEnemyExited;

        void Start()
        {
            floatingIcon.SetActive(true);
            currentState = EnemyStates.Waiting; 
            aipath = GetComponent<AIPath>();
        }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        doorC = GameObject.FindGameObjectWithTag("DoorC").transform;

    }

    
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Waiting:
                
                patienceTimer -= Time.deltaTime;
                if (patienceTimer <= 0f)
                {
                    EnterChasing(); 
                }
                break;

            case EnemyStates.Chasing:
                aIDestinationSetter.target = player;

                break;

            case EnemyStates.Exiting:
        
                if (Vector2.Distance(transform.position, doorC.position) < 0.2f)
                {
                    Destroy(gameObject);
                }
                break;
        }
        
    }

    private void EnterChasing()
    {
        currentState = EnemyStates.Chasing;
         aipath.maxSpeed = enemyChasingSpeed; 
    }

    public void EnterExiting()
    {
        currentState = EnemyStates.Exiting;
        aIDestinationSetter.target = doorC;
        
            OnEnemyExited?.Invoke(this);

        aipath.maxSpeed = enemyExitingSpeed; 
    }

    public void EnterCalming()
{
    currentState = EnemyStates.Calming;
    floatingIcon.SetActive(false);
    spriteRenderer.color = Color.white;
    aipath.canMove = false;

    StartCoroutine(CalmingRoutine());
}

private IEnumerator CalmingRoutine()
{
    yield return new WaitForSeconds(calmingDuration);
    aipath.canMove = true;
    EnterExiting(); 
}


    public void SetPatienceTimer(float timer)
    {
        patienceTimer = timer;
    }


    

}
