using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestEvent1 : Event
{
    public TestEvent1(float probability)
    {
        this.Probability = probability;
    }

    public override void ExecuteEvent()
    {
        if (EventEmergency)
        {
            
            EventManager.Instance.IncrementEmergencyEventCount();
        }
        EventManager.Instance.IncrementEventCount();
        Debug.Log("Evento activado");
        IsSpawned = true;
    }

    public override void StopEventExecution()
    {
        if (EventEmergency)
        {
            EventManager.Instance.DecrementEmergencyEventCount();
        }
        EventManager.Instance.DecrementEventCount();
       
        IsSpawned = false;
        EventEmergency = false;
        Debug.Log("Evento desactivado");
    }
}

