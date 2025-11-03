// EventSpawner.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    public static EventSpawner Instance { get; private set; }

    [SerializeField] private bool spawnActivate = true;
    [SerializeField] private Event[] events = new Event[0];

    [SerializeField] private float spawnInterval = 15f; // configurable desde inspector

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

    private void Start()
    {
        //  usar factory: crear y pasar eventos aquí
        // eventFactory.Register(...) / eventFactory.Create(...)
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (spawnActivate)
            {
                TrySpawn();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Exposición controlada (lectura)
    public IReadOnlyList<Event> GetEvents() => events;  

    // Setter público 
    public void SetEvents(Event[] newEvents)
    {
        events = newEvents ?? new Event[0];
    }

    private void TrySpawn()
    {
        if (events == null || events.Length == 0) return;

        // Calcular suma de probabilidades solo de los que no están spawn y no nulos
        float total = 0f;
        for (int i = 0; i < events.Length; i++)
        {
            var e = events[i];
            if (e == null) continue;
            // solo considerar eventos no spawn
            if (!e.IsSpawned)
            {
                total += Mathf.Max(0f, e.GetProbability());
            }
        }

        if (total <= 0f)
        {
            // nada para spawnear
            return;
        }

        // elegir un valor aleatorio entre 0 y total
        float r = UnityEngine.Random.value * total;
        float cumulative = 0f;

        for (int i = 0; i < events.Length; i++)
        {
            var e = events[i];
            if (e == null || e.IsSpawned) continue;

            float prob = Mathf.Max(0f, e.GetProbability());
            cumulative += prob;
            if (r <= cumulative)
            {
                // Se escogió el evento e
                Debug.Log($"Spawning event (prob {prob}) index {i}");
                e.ExecuteEvent();

                

                // Ajustar probabilidades: disminuir la elegida y aumentar las demás
                float decrease = prob / 2f;
                e.DecreaseProbability(decrease);

                
                float refillAmount = prob / Mathf.Max(1, events.Length);
                RefillProbabilities(refillAmount, i);

                return;
            }
        }
    }

    private void RefillProbabilities(float amountPerOther, int indexOfChosen)
    {
        for (int j = 0; j < events.Length; j++)
        {
            if (j == indexOfChosen) continue;
            var e = events[j];
            if (e == null) continue;
            e.IncreaseProbability(amountPerOther);
        }
    }

    // métodos públicos para activar/desactivar spawn
    public void SetSpawnActive(bool active) => spawnActivate = active;
    public bool IsSpawnActive() => spawnActivate;
}
