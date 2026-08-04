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
     [SerializeField] public bool exiting;
     [SerializeField] private float calmingDuration = 1f;
     [SerializeField] private float enemyExitingSpeed = 5f;
     [SerializeField] private float enemyChasingSpeed = 1.5f;
     [SerializeField] private float patienceTimer = 4f;

     public EnemyStates currentState;


     [SerializeField] private SpriteRenderer spriteRenderer;
     [SerializeField] private GameObject floatingIcon;
     [SerializeField] private EnemySpawner spawner;


        void Start()
        {
            floatingIcon.SetActive(true);
            currentState = EnemyStates.Waiting; 

            spawner = GetComponentInParent<EnemySpawner>();
            aipath = GetComponent<AIPath>();
        }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        doorC = GameObject.FindGameObjectWithTag("DoorC").transform;

    }

    // Update is called once per frame
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
            // EnterCalming();

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

    public void EnemyExits()
    {
        currentState = EnemyStates.Exiting;
        exiting = true;
        //floatingIcon.SetActive(false);
        //spriteRenderer.color = Color.white;
        aIDestinationSetter.target = doorC;
        Debug.Log("Enemy Exits");
    
        if(spawner != null)
        {
            spawner.RegisterMonsterHealed();
        }
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
    EnemyExits(); 
}

    public void SetSpawner(EnemySpawner spawnerREF)
    {
        spawner = spawnerREF;
    }

    public void SetPatienceTimer(float timer)
    {
        patienceTimer = timer;
    }


    // private void UpdateSprite()
    // {
    //     Vector2 vel = aipath.desiredVelocity;
    //     if (vel.y > 0); // up
    //     else if (vel.y < 0); // down

    //     if (vel.x > 0); // right
    //     else if (vel.x < 0); // left
    // }
}
