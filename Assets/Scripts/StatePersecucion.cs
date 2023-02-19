using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePersecucion : StateDrivenFSM
{
    public override void Execute(CAgent agent)
    {
        Vector3 currTarget = agent.soundTargetPosition;
        if (agent.playerHeard)
        {
            agent.soundTargetPosition = agent.target.position;
            agent.timerToChase = agent.timeInChase;
            currTarget = agent.soundTargetPosition;
        }
        
        if (agent.playerSeen)
        {
            agent.timerToChase = agent.timeInChase;
            currTarget = agent.target.position;
            agent.soundTargetPosition = currTarget;
        }

        agent.timerToChase -= Time.deltaTime;

        if (agent.timerToChase < 0)
        {
            agent.timerToFind = agent.timeInAlert;
            agent.spot.color = Color.yellow;
            agent.currentState = new StateAlerta();
        }

        agent.navAgent.destination = currTarget;
    }
}
