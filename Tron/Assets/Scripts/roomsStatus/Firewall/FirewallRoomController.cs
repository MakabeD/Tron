using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallRoomController : MonoBehaviour
{
    int id = 1;
    public float timer = 0;
    bool emergency;
    bool isSpawned;
    public Renderer renderLampColor;
    public static FirewallRoomController Instance;
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
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // 1) Validaciones básicas: EventManager y el evento (ev) deben existir
        if (EventManager.Instance == null) return;
        var ev = EventManager.Instance.GetEvent(id);
        if (ev == null) return;

        // 2) Obtener estados locales (lectura una sola vez por frame)
        bool isSpawned = ev.IsSpawned;
        bool emergency = ev.EventEmergency;

        // 3) Determinar color objetivo (prioridad: emergency -> spawned -> default)
        Color targetColor = Color.white;
        if (emergency)
        {
            targetColor = Color.red;
        }
        else if (isSpawned)
        {
            targetColor = Color.yellow;
        }
        // Si renderLampColor es null, no intentamos acceder al material
        if (renderLampColor != null)
        {
            renderLampColor.material.color = targetColor;
        }

        // 4) Lógica del temporizador: solo acumula si está spawned y no hay emergencia
        if (isSpawned && !emergency)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // Si el objeto no está spawned o hay emergencia, reiniciamos el timer
            timer = 0f;
            return;
        }

        // 5) Si el timer supera el umbral y aún no hay emergency en la fuente, activamos
        if (timer >= 50f && !ev.EventEmergency) 
        {
            ev.EventEmergency = true;
            EventManager.Instance.IncrementEmergencyEventCount();
            GameManager.Instance.isinCombat++;
            Debug.Log("Emergencia");
        }
    }

    public float getEmergencyTimer()
    {
        if (!emergency) return timer;
        return 50;
    }
}
