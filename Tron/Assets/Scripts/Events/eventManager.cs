using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    // Contadores encapsulados
    public int EventCount { get; private set; }
    public int EmergencyEventCount { get; private set; }

    // Eventos públicos que notifican cambios (invocados cuando cambia el contador)
    public event Action<int> OnEventCountChanged;
    public event Action<int> OnEmergencyEventCountChanged;

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

    void Start()
    {
        //factory
        var factory = new EventFactory();
        // Registrado dentro de EventFactory constructor; aquí se muestra uso:
        Event[] initial = new Event[] { factory.Create("TestEvent1", 1f) };
        EventSpawner.Instance.SetEvents(initial);

        EventCount = 0;
        EmergencyEventCount = 0;
    }

    public Event GetEvent(int x)
    {
        return EventSpawner.Instance.GetEvents()[x];
    }

    // Métodos que modifican contadores y notifican (aquí invocamos los eventos)
    public void IncrementEventCount()
    {
        EventCount++;
        OnEventCountChanged?.Invoke(EventCount);
    }

    public void DecrementEventCount()
    {
        EventCount = Mathf.Max(0, EventCount - 1);
        OnEventCountChanged?.Invoke(EventCount);
    }

    public void IncrementEmergencyEventCount()
    {
        EmergencyEventCount++;
        OnEmergencyEventCountChanged?.Invoke(EmergencyEventCount);
    }

    public void DecrementEmergencyEventCount()
    {
        EmergencyEventCount = Mathf.Max(0, EmergencyEventCount - 1);
        OnEmergencyEventCountChanged?.Invoke(EmergencyEventCount);
    }

    void Update()
    {
        
    }
}
