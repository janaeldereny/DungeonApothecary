using UnityEngine;
using Pathfinding;

public class AstarAI : MonoBehaviour
{
    public Transform targetPosition;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;

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

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateInterval);
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

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}