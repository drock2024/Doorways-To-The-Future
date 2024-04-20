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

    // Start is called before the first frame update
    void Start() {
        
        state = AIState.ROAM;
        agent = GetComponent<NavMeshAgent>();
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
    
        Vector3 diff = lookaheadDistance * new Vector3(-1, 0, 0);
        Vector3 dest = transform.position + diff;

        // TODO: Add terrain avoidance/boundaries

        agent.SetDestination(dest);
        Debug.Log($"Curr: {transform.position} | Diff: {diff} | Dest: {dest}");

    } // SetNextWaypoint

} // FishAIController