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
            agent.currentState = new StatePersecucion();
        }
        else if ( agent.playerHeard )
        {
            agent.timerToFind = agent.timeInAlert;
            agent.soundTargetPosition = agent.transform.position;
            agent.currentState = new StateAlerta();
        }
        else
        {
            
        }
    }
}
