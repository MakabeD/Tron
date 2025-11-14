using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot1Behaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform destination;

    

    void Start()
    {
       
        
    }

    void Update()
    {
        if (destination == null) return;
        agent.destination = destination.position;

        // para destruir mas adelante
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GameHP.Instance.GetDownLife(1);
            gameObject.SetActive(false);
            
        }
        
    }
}

