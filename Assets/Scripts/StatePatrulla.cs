using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulla : StateDrivenFSM
{
    public override void Execute(CAgent agent)
    {
        if (agent.playerSeen)
        {
            agent.timerToChase = agent.timeInChase;
            agent.spot.color = Color.red;
            agent.currentState = new StatePersecucion();
        }
        else if ( agent.playerHeard )
        {
            agent.timerToFind = agent.timeInAlert;
            agent.soundTargetPosition = agent.transform.position;
            agent.spot.color = Color.yellow;
            agent.navAgent.destination = agent.transform.position;
            agent.currentState = new StateAlerta();
        }
        else
        {
            Vector3 IndexPos = agent.patrolPoints[agent.patrolIndex].position;
            if( Vector3.Distance(agent.transform.position, IndexPos ) < 0.5 )
            {
                if(agent.patrolIndex == agent.patrolPoints.Count-1)
                {
                    agent.patrolIndex = -1;
                }

                agent.patrolIndex++;
                agent.navAgent.destination = agent.patrolPoints[agent.patrolIndex].position;
            }
        }
    }
}
