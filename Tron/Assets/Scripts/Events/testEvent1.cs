using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent1 : Event
{
    

    public override void ExcecuteEvent()
    {
        eventManager.Instance.eventCount++;
        Debug.Log("evento activado");
    }
    public override void stopEventExcecute()
    {
        eventManager.Instance.eventCount--;
        isSpawned = false;
        Debug.Log("evento desactivado");
    }
    public testEvent1( float probability)
    {
        
        this.probability = probability;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
