using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTerminarInteraction : interactibeObject
{
    public Transform playerTransform;
    public playerMovement playerMovement;
    
    int id = 1;
    public GameObject minigamePrefab;
    public override void Interact()
    {
        if (EventManager.Instance.GetEvent(id).IsSpawned && !GameManager.Instance.isInGame && GameManager.Instance.isinCombat==0)
        {
            openMiniGame(); 

        }
    }
    void openMiniGame()
    {

        //acomodando al jugador al frente del panel
        playerTransform.position = new Vector3(transform.position.x+1.5f, playerTransform.position.y, transform.position.z);
        playerTransform.rotation = Quaternion.Euler(0, -90, 0);
        MinigameManager.Instance.StartMinigame(minigamePrefab);
    }
    void closeMiniGame()
    {
        
        
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
