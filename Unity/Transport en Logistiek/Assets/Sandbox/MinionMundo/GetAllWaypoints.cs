using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllWaypoints : MonoBehaviour
{
    [Header("Waypoint stuff")]
    public List<GameObject> waypoints = new List<GameObject>();
    public GameObject waypointParent;

    [Header("Collision Prefab")]
    public GameObject collisionPrefab;

    public void Start()
    {
        GetAllDestinations();
    }

    public void GetAllDestinations()
    {
        int waypointCount = waypointParent.transform.childCount;
        for (int i = 0; i < waypointCount; i++)
        {
            waypoints.Add(waypointParent.transform.GetChild(i).gameObject);
        }
    }
}
