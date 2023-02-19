using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CAgent : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public List<Transform> PatrolPoints;
    public Transform target;
    public LayerMask layerPlayer;
    public float speed;
    public float rotateSpeed;
    public float soundDistance;
    public float viewDistance;
    public float timeInAlert;
    public float timeInChase;

    [HideInInspector]public StateDrivenFSM currentState;
    [HideInInspector]public float timerToFind;
    [HideInInspector]public float timerToChase;
    [HideInInspector] public Vector3 soundTargetPosition;
    private int nextPatrolIndex;
    [HideInInspector] public bool playerHeard, playerSeen;
    [HideInInspector] public Vector3 lastFramePosAgent;
    private RaycastHit hit;
    
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        currentState = new StatePatrulla();
        lastFramePosAgent = transform.position;
        playerHeard = false;
        playerSeen = false;
    }

    void Update()
    {
        UpdateSenses();
        currentState.Execute(this);
    }

    void UpdateSenses()
    {
        playerHeard = false;
        playerSeen = false;

        if (Vector3.Distance(target.position, transform.position) < soundDistance)
        {
            if (lastFramePosAgent != target.position)
            {
                playerHeard = true;
            }
        }
        

        Vector3 localTarget = transform.InverseTransformPoint(target.transform.position);
        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        //Debug.Log(targetAngle);
        if (targetAngle < 30 && targetAngle > -30)
        {
            Vector3 rayOrigin = transform.position + new Vector3(0, 0.5f, 0);
            Vector3 directionRay = target.transform.position - transform.position;
            Ray rayFordward = new Ray(rayOrigin, directionRay);
            if (Physics.Raycast(rayFordward, out hit, viewDistance))
            {
                if (hit.collider.gameObject.layer == layerPlayer)
                {
                    playerSeen = true;
                    Debug.Log("te veo");
                }
            }
        }

        
    }
}
