using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCubeInteraction : interactibeObject
{
    private int id=0;
    public override void Interact()
    {
        //GameHP.Instance.GetDownLife(10);
        if(eventManager.Instance.GetEvent(id).isSpawned)
        {
            eventManager.Instance.GetEvent(id).stopEventExcecute();
        }
        //Destroy(gameObject);
    }
}
