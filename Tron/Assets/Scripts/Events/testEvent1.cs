using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent1 : Event
{
    
    

    //public event Action yellowRedAletrsTestEvent1;
    public override void ExcecuteEvent()
    {
        if (eventEmergency) eventManager.Instance.eventEmergiCount++;
        eventManager.Instance.eventCount++;
        Debug.Log("evento activado");
    }
    public override void stopEventExcecute()
    {
        if(eventEmergency) eventManager.Instance.eventEmergiCount--;
        eventManager.Instance.eventCount--;
        isSpawned = false;
        eventEmergency = false;
        Debug.Log("evento desactivado");
    }
    public testEvent1( float probability)
    {
        
        this.probability = probability;
    }
    
}
