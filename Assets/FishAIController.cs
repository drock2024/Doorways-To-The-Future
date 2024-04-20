using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState { ROAM }

[RequireComponent(typeof(NavMeshAgent))]
public class FishAIController : MonoBehaviour {

    AIState state;
    private NavMeshAgent agent;

    [SerializeField] private float lookaheadDistance;
    [SerializeField] private Vector2 xRange;
    [SerializeField] private Vector2 zRange;

    [SerializeField] private GameObject destinationTracker;
    [SerializeField] private Vector3[] waypoints;
    private int currWaypoint;

    // Start is called before the first frame update
    void Start() {
        
        state = AIState.ROAM;
        agent = GetComponent<NavMeshAgent>();
        currWaypoint = -1;
        SetNextWaypoint();

    } // Start

    // Update is called once per frame
    void Update() {

        switch (state) {

            case AIState.ROAM:

                // Patrol state behavior
                if (!agent.pathPending && agent.remainingDistance == 0) {                    
                    SetNextWaypoint();

                } // if

                break;

            default:
                break;

        } // switch

        if (destinationTracker != null)
            destinationTracker.transform.position = agent.destination;
    
    } // Update

    private void SetNextWaypoint() {

        if (waypoints != null && waypoints.Length > 0) {
            
            int len = waypoints.Length;
            currWaypoint = (currWaypoint + 1) % len;
            agent.SetDestination(waypoints[currWaypoint]);

        } // if

    } // SetNextWaypoint

} // FishAIController