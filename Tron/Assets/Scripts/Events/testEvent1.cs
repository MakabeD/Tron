using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Event
{

    public float probability = 0f;
    public bool isSpawned = false;
    public bool eventEmergency = false;




    public abstract void ExcecuteEvent();
    public abstract void stopEventExcecute();

    public float getProbability() { return probability; }

    public void getDownProbability(float x)
    {
        probability -= x;
    }

    public void getUpProbability(float x)
    {
        probability += x;
    }


}
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
