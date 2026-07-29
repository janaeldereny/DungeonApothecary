using UnityEngine;
using Pathfinding;
using System.Collections;

public class AstarAI : MonoBehaviour
{
    public Transform targetPosition;

    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    public Path path;

    public Transform exitPoint;
    public float detectionRange = 5f;
    public float losePlayerRange = 10f;
    public float angryDuration = 1.5f;
    public float calmingDuration = 2.0f;
     public MonsterState currentState = MonsterState.Waiting;

    public float speed = 2;
    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;
    public bool reachedEndOfPath;

    // public void Start()
    // {
    //     seeker = GetComponent<Seeker>();
    //     rb = GetComponent<Rigidbody2D>();

    //     seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    // }

    public float pathUpdateInterval = 0.5f;

    public enum MonsterState
    {
        Waiting,
        Angry,
        Chasing,
        Calming,
        Exiting
    }

    public void Start()
    {
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponentInChildren<Animator>();
        Debug.Log(animator.gameObject.name);
        //animator= GetComponent<Animator>();

        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateInterval);
   
        //   if (targetPosition == null)
        // {
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     if (player != null)
        //     {
        //         targetPosition = player.transform;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Player not found! Make sure the Player GameObject has the 'Player' tag.");
        //     }
        // }
    }

    //  private void Update()
    // {
    //     // Handle state triggers and distance logic
    //     switch (currentState)
    //     {
    //         case MonsterState.Waiting:
    //             if (targetPosition != null && Vector2.Distance(rb.position, targetPosition.position) <= detectionRange)
    //             {
    //                 TransitionToState(MonsterState.Angry);
    //             }
    //             break;

    //         case MonsterState.Angry:
    //             // Handled via AngryRoutine coroutine
    //             break;

    //         case MonsterState.Chasing:
    //             if (targetPosition != null && Vector2.Distance(rb.position, targetPosition.position) > losePlayerRange)
    //             {
    //                 TransitionToState(MonsterState.Calming);
    //             }
    //             break;

    //         case MonsterState.Calming:
    //             // Handled via CalmingRoutine coroutine
    //             break;

    //         case MonsterState.Exiting:
    //             if (exitPoint != null && Vector2.Distance(rb.position, exitPoint.position) < 0.5f)
    //             {
    //                 Destroy(gameObject);
    //             }
    //             break;
    //     }
    // }

    void Update()
    {
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            //  Vector3 clampedTarget = AstarPath.active.GetNearest(targetPosition.position, NNConstraint.Default).position;
            //   seeker.StartPath(rb.position, clampedTarget, OnPathComplete); 
            seeker.StartPath(rb.position, targetPosition.position, OnPathComplete);
        }
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        //     else
        // {
        //     Debug.Log("Path failed: " + p.errorLog);
        //     // optionally: path = null; to stop movement rather than run a stale path
        // }
    }

    public void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        reachedEndOfPath = false;
        float distanceToWaypoint;

        while (true)
        {
            distanceToWaypoint = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velocity = dir * speed * speedFactor;

        if(!reachedEndOfPath && velocity.sqrMagnitude > 0.01f)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            SetWalkingAnimation(true,dir);
        }
        else
        {
            SetWalkingAnimation(false,Vector2.zero);
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void SetWalkingAnimation(bool isWalking, Vector2 direction)
    {
        if (animator== null) return;
        animator.SetBool ("isWalking",isWalking);

        if (isWalking)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);

             animator.SetFloat("lastmoveX", direction.x);
            animator.SetFloat("lastmoveY", direction.y);
        }
    }

    //  public void TransitionToState(MonsterState newState)
    // {
    //     currentState = newState;

    //     switch (newState)
    //     {
    //         case MonsterState.Angry:
    //             StartCoroutine(AngryRoutine());
    //             break;

    //         case MonsterState.Calming:
    //             StartCoroutine(CalmingRoutine());
    //             break;
    //     }
    // }

    // private IEnumerator AngryRoutine()
    // {
    //     path = null; // Reset path during anger pause
    //     if (animator != null) animator.SetTrigger("isAngry"); // Triggers optional roar animation

    //     yield return new WaitForSeconds(angryDuration);

    //     TransitionToState(MonsterState.Chasing);
    // }

    // private IEnumerator CalmingRoutine()
    // {
    //     path = null; // Reset path during calm pause
    //     if (animator != null) animator.SetTrigger("isCalming"); // Triggers optional calming animation

    //     yield return new WaitForSeconds(calmingDuration);

    //     TransitionToState(MonsterState.Exiting);
    // }


    //     public void CureMonster(Transform doorTransform = null)
    // {
    

    //     // Stop any ongoing Angry or Calming coroutines immediately
    //     StopAllCoroutines();

    //     // Transition directly to Exiting
    //     TransitionToState(MonsterState.Exiting);
    // }

}










   

    

   
   
