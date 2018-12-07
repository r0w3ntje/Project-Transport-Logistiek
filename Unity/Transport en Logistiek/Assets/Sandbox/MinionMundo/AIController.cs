using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //[Header("Waypoint stuff")]
    //public List<GameObject> waypointEnds = new List<GameObject>();
    //public GameObject waypointEnd;
    [Header("Destinations")]
    public Transform destination;
    public GameObject currentWaypoint;
    public Vector3 startLocation;

    [Header("External Scripts")]
    //public AIInfoInput aiInfoInput;

    public GetAllWaypoints getWP;

    private NavMeshAgent agent;
    private NavMeshPath navMeshPath;
    private GameObject collisionPrefab;
    private Transform _destination
    {
        get { return getWP.waypoints[randomWaypoint].transform; }
    }

    private float time;
    private int randomWaypoint;
    private int randomTime;
    private bool hasDestination;

    void Start()
    {
        hasDestination = false;
        collisionPrefab = getWP.collisionPrefab;
       // aiInfoInput = GetComponent<AIInfoInput>();

        randomWaypoint = Random.Range(0, getWP.waypoints.Count);
        randomTime = Random.Range(1, 5);

        startLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(WaitForDestination(randomTime));
    }

    void Update()
    {
        CheckCurrentPosition();    
    }

    void SetDestination()
    {
        destination = _destination;
        currentWaypoint = destination.gameObject;
        //aiInfoInput.Goingto_Holder = destination.ToString();
        agent.SetDestination(_destination.position);
        getWP.waypoints.Remove(currentWaypoint);
    }

    void CheckCurrentPosition()
    {
        if (!agent.pathPending && !agent.hasPath && hasDestination)
        {
            agent.SetDestination(startLocation);
            int randomWaitingTime = Random.Range(1, 120);
            StartCoroutine(WaitAtDestination(randomWaitingTime));
            hasDestination = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int randomWaitingTime = Random.Range(1, 120);
        StartCoroutine(WaitForDestination(randomWaitingTime));
    }


    IEnumerator WaitForDestination(int t)
    {
        yield return new WaitForSeconds(t);
        SetDestination();
        hasDestination = true;
    }

    IEnumerator WaitAtDestination(int t)
    {
        Instantiate(collisionPrefab, startLocation, Quaternion.identity);

        yield return new WaitForSeconds(t);
        agent.SetDestination(startLocation);
        getWP.waypoints.Add(currentWaypoint);
    }
}