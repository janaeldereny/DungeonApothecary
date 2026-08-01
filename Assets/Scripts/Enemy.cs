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
     [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (!exiting)
        {
         aIDestinationSetter.target = player;
        }

         if (exiting &&
            Vector2.Distance(transform.position, DoorC.position) < 0.2f)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyExits()
    {
        exiting = true;
        spriteRenderer.color = Color.white;
        aIDestinationSetter.target = DoorC;
        Debug.Log("Enemy Exits");
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
