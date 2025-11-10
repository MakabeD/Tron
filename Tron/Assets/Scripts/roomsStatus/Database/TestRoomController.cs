using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomController : MonoBehaviour
{
    // Singleton seguro (setter privado)
    public static TestRoomController Instance { get; private set; }

    [SerializeField] private int id = 0;
    [SerializeField] private float timer = 0f;

    
    private bool emergency;
    private bool isSpawned;

    [SerializeField] private Renderer renderLampColor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
        var evt = EventManager.Instance.GetEvent(id);
        if (evt == null) return;

        isSpawned = evt.IsSpawned;
        emergency = evt.EventEmergency;

        
        if (renderLampColor != null)
        {
            if (emergency)
            {
                renderLampColor.material.color = Color.red;
            }
            else if (isSpawned)
            {
                renderLampColor.material.color = Color.yellow;
            }
            else
            {
                renderLampColor.material.color = Color.white;
            }
        }

        
        if (isSpawned && !emergency)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            return;
        }

        // Al pasar 60s activamos la emergencia para este evento
        if (timer >= 60f && !evt.EventEmergency)
        {
            GameHP.Instance.GetDownLife(10);

            evt.SetEmergency(true);

            GameManager.Instance.isinCombat++;

            EventManager.Instance.IncrementEmergencyEventCount();
            Debug.Log("emergencia por tiempo(database)");
        }
    }

    
    public float GetEmergencyTimer() => !emergency ? timer : 60f;
    public void IncreaseEmergencyTimer(int i) => timer += i;
}
