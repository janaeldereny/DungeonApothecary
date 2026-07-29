using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;  
    private Transform player;
    [SerializeField]private AIDestinationSetter aIDestinationSetter;
    [SerializeField] private AIPath aipath;
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
         aIDestinationSetter.target = player;
    }
    private void UpdateSprite()
    {
        Vector2 vel = aipath.desiredVelocity;
        if (vel.y > 0); // up
        else if (vel.y < 0); // down

        if (vel.x > 0); // right
        else if (vel.x < 0); // left
    }
}
