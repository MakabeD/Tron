using System;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    // Singleton seguro (setter privado)
    public static MinigameManager Instance { get; private set; }

    [SerializeField] private Transform uiRoot;
    public Transform UIRoot => uiRoot;
    
    public GameObject CurrentMinigameGO { get; private set; }
    public IMinigame CurrentMinigame { get; private set; }

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
        CurrentMinigameGO = Instantiate(minigamePrefab, uiRoot, worldPositionStays: false);
        CurrentMinigame = CurrentMinigameGO.GetComponent<IMinigame>();
        CurrentMinigame?.StartMinigame();

        Cursor.lockState = CursorLockMode.None;

        OnMinigameStarted?.Invoke();
    }

    // Cerrar y limpiar
    public void EndMinigame()
    {
        if (!GameManager.Instance.isInGame) return;

        CurrentMinigame?.EndMinigame();

        if (CurrentMinigameGO != null) Destroy(CurrentMinigameGO);
        CurrentMinigameGO = null;
        CurrentMinigame = null;

        GameManager.Instance.isInGame = false;

        Cursor.lockState = CursorLockMode.Locked;

        OnMinigameEnded?.Invoke();
    }
}
