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

        Vector2 center = new Vector2((xRange.x + xRange.y) / 2, (zRange.x + zRange.y) / 2);
        float distanceRatioCenterX = (center.x - transform.position.x) / (xRange.y - xRange.x);
        float distanceRatioCenterZ = (center.y - transform.position.z) / (zRange.y - zRange.x);
        float distanceRatioFromCenter = new Vector2(distanceRatioCenterX, distanceRatioCenterZ).magnitude;

        int directionChangeChance = (int)(80 * (1.0f - distanceRatioFromCenter));
        bool directionChange = ((int) Random.Range(0, 99)) >= directionChangeChance;

        int angle = (directionChange) ? ((int) Random.Range(0, 359)) : 0;

        // Debug.Log($"Set next waypoint | Angle: {transform.rotation.y}");
        Vector3 dest = GetDestination(angle);
        int direction = (((int) Random.Range(0, 99)) >= 50) ? 1 : -1;

        while (Physics.CheckSphere(dest, transform.position.y - 0.1f)) {
            angle += 5 * direction;
            dest = GetDestination(angle);

        } // while

        agent.SetDestination(dest);
        // Debug.Log($"Curr: {transform.position} | Dest: {dest}");

    } // SetNextWaypoint

    private Vector3 GetDestination(int angleInDegrees) {

        Vector3 diff = lookaheadDistance * new Vector3(
            Mathf.Sin((1.0f / Mathf.Rad2Deg) * (transform.eulerAngles.y + angleInDegrees)), 
            0, 
            Mathf.Cos((1.0f / Mathf.Rad2Deg) * (transform.eulerAngles.y + angleInDegrees))
        );
        return transform.position + diff;
        
    } // GetDestination

} // FishAIController