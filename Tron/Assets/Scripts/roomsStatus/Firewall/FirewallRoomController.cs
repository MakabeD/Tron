using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallRoomController : MonoBehaviour
{
    // Singleton seguro
    public static FirewallRoomController Instance { get; private set; }

    [SerializeField] private int id = 1;
    [SerializeField] private float timer = 0f;

    [SerializeField] private Renderer renderLampColor;

    // Umbral configurable (puedes exponerlo si quieres tunear desde Inspector)
    private const float EmergencyThreshold = 50f;

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
        
        if (EventManager.Instance == null) return;
        var ev = EventManager.Instance.GetEvent(id);
        if (ev == null) return;

        
        bool isSpawned = ev.IsSpawned;
        bool emergency = ev.EventEmergency;

        
        Color targetColor = Color.white;
        if (emergency) targetColor = Color.red;
        else if (isSpawned) targetColor = Color.yellow;

        if (renderLampColor != null)
        {
            renderLampColor.material.color = targetColor;
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

        
        if (timer >= EmergencyThreshold && !ev.EventEmergency)
        {
            
            ev.SetEmergency(true);

            EventManager.Instance.IncrementEmergencyEventCount();
            GameManager.Instance.isinCombat++;
            Debug.Log("Emergencia");
        }
    }

   
    public float GetEmergencyTimer()
    {
        
        if (EventManager.Instance == null) return timer;
        var ev = EventManager.Instance.GetEvent(id);
        if (ev == null) return timer;

        return ev.EventEmergency ? EmergencyThreshold : timer;
    }
}
