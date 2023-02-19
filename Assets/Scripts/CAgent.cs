using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CAgent : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public List<Transform> patrolPoints;
    public Transform target;
    public float rotateSpeed, soundDistance, viewDistance, timeInAlert, timeInChase;
    public GameObject spotLigth;

    [HideInInspector]public StateDrivenFSM currentState;
    [HideInInspector]public float timerToFind, timerToChase;
    [HideInInspector] public Vector3 soundTargetPosition, lastFramePosAgent;
    [HideInInspector] public int patrolIndex;
    [HideInInspector] public bool playerHeard, playerSeen;
    [HideInInspector] public Light spot;

    
    void Start()
    {
        spot = spotLigth.GetComponent<Light>();
        navAgent = GetComponent<NavMeshAgent>();
        spot.color = Color.green;
        currentState = new StatePatrulla();
        patrolIndex = 0;
        navAgent.destination = patrolPoints[patrolIndex].position;
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
        if (targetAngle < 30 && targetAngle > -30)
        {
            Vector3 rayOrigin = transform.position + new Vector3(0, 0.5f, 0);
            Vector3 directionRay = target.transform.position - transform.position;
            Ray rayFordward = new Ray(rayOrigin, directionRay);
            RaycastHit hit;
            if (Physics.Raycast(rayFordward, out hit, viewDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    playerSeen = true;
                }
            }
        }
    }

}
