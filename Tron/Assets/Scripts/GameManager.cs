using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public bool isInGame =false;
    public int isinCombat;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void stopSpawn()
    {
        EventManager.Instance.StopSpwaner();
    }
    public void EndLevel()
    {
        stopSpawn();
        EventManager.Instance.Reset();
        MinigameManager.Instance.EndMinigame();

    }

    // Start is called before the first frame update
    void Start()
    {
        isinCombat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
