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
        Debug.Log($"Angle: {transform.eulerAngles.y} ({(1.0f / Mathf.Rad2Deg) * transform.eulerAngles.y} Radians)");
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
        Debug.Log($"Set next waypoint | Angle: {transform.rotation.y}");
        Vector3 diff = lookaheadDistance * new Vector3(
            Mathf.Sin((1.0f / Mathf.Rad2Deg) * transform.eulerAngles.y), 
            0, 
            Mathf.Cos((1.0f / Mathf.Rad2Deg) * transform.eulerAngles.y)
        );
        Vector3 dest = transform.position + diff;

        // TODO: Add terrain avoidance/boundaries

        agent.SetDestination(dest);
        Debug.Log($"Curr: {transform.position} | Diff: {diff} | Dest: {dest}");

    } // SetNextWaypoint

} // FishAIController