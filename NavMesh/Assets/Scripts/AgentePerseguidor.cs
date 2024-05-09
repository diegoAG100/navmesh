using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentePerseguidor : MonoBehaviour
{
       public Transform goal;
       private NavMeshAgent agent;

    void Start () {
          agent = GetComponent<NavMeshAgent>();
    }
    
    void Update(){
        agent.SetDestination(goal.position);
    }

    public void StopFollow(){
        agent.ResetPath();
    }
}
