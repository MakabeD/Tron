using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    public Transform uiRoot; // asigna el Canvas root donde instanciar (p. ej. un GameObject vacío dentro del Canvas)
    public GameObject currentMinigameGO { get; private set; }
    public IMinigame currentMinigame { get; private set; }

    public event Action OnMinigameStarted;
    public event Action OnMinigameEnded;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // prefab debe contener un componente que implemente IMinigame (ej. SimpleClickMinigame)
    public void StartMinigame(GameObject minigamePrefab)
    {
        if (GameManager.Instance.isInGame) return;

        GameManager.Instance.isInGame = true;
        // instanciar UI
        currentMinigameGO = Instantiate(minigamePrefab, uiRoot, worldPositionStays: false);
        currentMinigame = currentMinigameGO.GetComponent<IMinigame>();
        currentMinigame?.StartMinigame();

        
        Cursor.lockState = CursorLockMode.None;

        OnMinigameStarted?.Invoke();
    }

    // Cerrar y limpiar
    public void EndMinigame()
    {
        if (!GameManager.Instance.isInGame) return;

        

        currentMinigame?.EndMinigame();

        if (currentMinigameGO != null) Destroy(currentMinigameGO);
        currentMinigameGO = null;
        currentMinigame = null;

        GameManager.Instance.isInGame = false;

        
        Cursor.lockState = CursorLockMode.Locked;

        OnMinigameEnded?.Invoke();
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
