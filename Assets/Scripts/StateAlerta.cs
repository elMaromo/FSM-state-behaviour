using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAlerta : StateDrivenFSM
{
    public override void Execute(CAgent agent)
    {
        
        if (agent.playerHeard)
        {
            agent.soundTargetPosition = agent.target.position;
        }
        
        agent.timerToFind -= Time.deltaTime;
        if (agent.timerToFind < 0)
        {
            agent.spot.color = Color.green;
            agent.navAgent.destination = agent.patrolPoints[agent.patrolIndex].position;
            agent.currentState = new StatePatrulla();
        }

        rotateAgent(agent);
        
        if (agent.playerSeen)
        {
            agent.spot.color = Color.red;
            agent.timerToChase = agent.timeInChase;
            agent.currentState = new StatePersecucion();
        }
    }

    private void rotateAgent(CAgent agent)
    {
        Vector3 posTarget = agent.soundTargetPosition;
        Vector3 posAgent = agent.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(posTarget - posAgent);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, agent.rotateSpeed * Time.deltaTime);
    }
}
