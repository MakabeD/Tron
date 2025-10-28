using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTerminarInteraction : interactibeObject
{
    public Transform playerTransform;

    public playerMovement playerMovement;
    
    public GameObject miniGameCanvas;
    public override void Interact()
    {
        if (playerMovement != null) playerMovement.canMove = false;
        
        openMiniGame();


    }
    void openMiniGame()
    {
        GameManager.Instance.isInGame = true;
        miniGameCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //acomodando al jugador al frente del panel
        playerTransform.position = new Vector3(transform.position.x+1.5f, playerTransform.position.y, transform.position.z);
        playerTransform.rotation = Quaternion.Euler(0, -90, 0);
    }
    void closeMiniGame()
    {
        GameManager.Instance.isInGame = false;
        miniGameCanvas.SetActive(false);
        playerMovement.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        miniGameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (miniGameCanvas.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            closeMiniGame();
        }
    }
}
