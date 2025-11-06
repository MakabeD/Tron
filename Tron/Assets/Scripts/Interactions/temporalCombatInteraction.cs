using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporalCombatInteraction : interactibeObject
{
    public int id;
    public override void Interact()
    {
        if (GameManager.Instance.isinCombat>0 && EventManager.Instance.GetEvent(id).EventEmergency)
        {
            EventManager.Instance.GetEvent(id).StopEventExecution();
        }
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
