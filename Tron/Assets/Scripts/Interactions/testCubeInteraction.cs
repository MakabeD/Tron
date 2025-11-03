using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCubeInteraction : interactibeObject
{
    private int id=0;
    public GameObject minigamePrefab;

    public override void Interact()
    {
        //GameHP.Instance.GetDownLife(10);
        if(EventManager.Instance.GetEvent(id).IsSpawned && !GameManager.Instance.isInGame&& !GameManager.Instance.isinCombat)
        {
            MinigameManager.Instance.StartMinigame(minigamePrefab);
            
        }
        //Destroy(gameObject);
    }
}
