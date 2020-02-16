using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAction : MonoBehaviour
{
    private const float FIELD_OF_VIEW_ANGLE = 110f;
    private const float HEAR_DISTANCE = 10f;

    private bool playerInSight;
    private bool playerInEarShot;

    private NavMeshAgent navMeshAgent;
    private SphereCollider sphereCollider;
    private Animator currentPlayerAnimation;
    private GameObject player;
    private GameObject[] plushies;

    
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        player = GameObject.Find("Player");
        currentPlayerAnimation = player.GetComponent<Animator>();
        plushies = new GameObject[20];
        // need to insert plushies into this array
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // check to see if guard triggered sphere collider around player
        if (other.gameObject == player)  // also need to add plushies
        {
            Vector3 direction = other.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // Check to see if player is in line of sight.
            if (angle < FIELD_OF_VIEW_ANGLE * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized,
                                   out hit, sphereCollider.radius))
                {
                    if(hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                    }
                    else
                    {
                        playerInSight = false;
                    }
                }
            }

            // Check to see if player is close enough to hear
            if(CalculatePathLength(player.transform.position) <= HEAR_DISTANCE)
            {
                playerInEarShot = true;
            }
            else
            {
                playerInEarShot = false;
            }
        }
    }

    // determines of guard is close enough to hear player
    private float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (navMeshAgent.enabled)
        {
            navMeshAgent.CalculatePath(targetPosition, path);
        }

        Vector3[] arrOfWayPoints = new Vector3[path.corners.Length + 2];

        arrOfWayPoints[0] = this.transform.position;
        arrOfWayPoints[arrOfWayPoints.Length - 1] = targetPosition;

        for(int i = 0; i < path.corners.Length; i++)
        {
            arrOfWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            pathLength += Vector3.Distance(arrOfWayPoints[i], arrOfWayPoints[i + 1]);
        }

        return pathLength;
    }
}
