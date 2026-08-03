using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;  
    private Transform player;
    [SerializeField]private AIDestinationSetter aIDestinationSetter;
    [SerializeField] private AIPath aipath;
    [SerializeField] private Transform DoorC;
     [SerializeField] public bool exiting;

     public EnemyStates currentState;
     private float patienceTimer;


     [SerializeField] private SpriteRenderer spriteRenderer;
     [SerializeField] private GameObject floatingIcon;
     [SerializeField] private EnemySpawner spawner;


        void Start()
        {
            floatingIcon.SetActive(true);
            currentState = EnemyStates.Waiting; 
        }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

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
            break;

        case EnemyStates.Exiting:
    
            if (Vector2.Distance(transform.position, DoorC.position) < 0.2f)
            {
                Destroy(gameObject);
            }
            break;
    }
        // if (!exiting)
        // {
        //  aIDestinationSetter.target = player;
        // }

        //  if (exiting &&
        //     Vector2.Distance(transform.position, DoorC.position) < 0.2f)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void EnterChasing()
    {
        currentState = EnemyStates.Chasing;
    }

    public void EnemyExits()
    {
        exiting = true;
        floatingIcon.SetActive(false);
        spriteRenderer.color = Color.white;
        aIDestinationSetter.target = DoorC;
        Debug.Log("Enemy Exits");
    
        if(spawner != null)
        {
            spawner.RegisterMonsterHealed();
        }
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
