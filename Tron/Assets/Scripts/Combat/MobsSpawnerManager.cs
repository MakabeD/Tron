using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSpawnerManager : MonoBehaviour
{
    [Header("Spawn config")]
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private GameObject[] Mobs;
    [SerializeField] private bool Enabled = true;

    [Header("Wave generation")]
    public Transform[] destination;
    [SerializeField] private int wavesToGenerate = 3;      // cuantas oleadas crear cuando se inicializa
    [SerializeField] private float minSpawnDelay = 0.5f;   // delay mínimo entre cápsulas en una oleada
    [SerializeField] private float maxSpawnDelay = 1.5f;   // delay máximo entre cápsulas en una oleada
    [SerializeField] private float interWaveDelay = 2f;    // delay entre oleadas

    // Estructura para las oleadas
    private class Wave
    {
        public int count;
        public float spawnDelay;

        public Wave(int count, float spawnDelay)
        {
            this.count = count;
            this.spawnDelay = spawnDelay;
        }
    }

    private Queue<Wave> wavesQueue = new Queue<Wave>();

    // Estado actual de spawning
    private Wave currentWave = null;
    private int spawnedInCurrentWave = 0;
    private float timer = 0f;
    private bool waitingBetweenWaves = false;

    void Start()
    {
        // Si quieres crear automáticamente la cola inicial:
        CreateRandomWaves(wavesToGenerate);
        if (wavesQueue.Count > 0)
        {
            StartWaves();
        }
    }

    void Update()
    {
        if (!Enabled) return;

        // Si no hay oleadas activas, nada que hacer
        if (currentWave == null)
        {
            return;
        }

        // Si estamos esperando el tiempo entre oleadas, contamos y retornamos
        if (waitingBetweenWaves)
        {
            
            timer += Time.deltaTime;
            if (timer >= interWaveDelay) // reutilizamos spawnDelay temporalmente para espera entre oleadas? mejor usar interWaveDelay
            {
                waitingBetweenWaves = false;
                timer = 0f;
            }
            return;
        }
        

        // Si quedan capsulas por spawnear en la oleada actual
        if (spawnedInCurrentWave < currentWave.count)
        {
            
            timer += Time.deltaTime;
            if (timer >= currentWave.spawnDelay)
            {
                
                
                SpawnCapsule();
                spawnedInCurrentWave++;
                timer = 0f;
            }
        }
        else
        {
            // Oleada completada: pasar a la siguiente oleada después de interWaveDelay
            
            if (wavesQueue.Count > 0)
            {
                
                // programamos la espera entre oleadas
                StartCoroutine(WaitThenNextWave(interWaveDelay));
            }
            else
            {
                // no hay más oleadas: dejamos currentWave = null para detener spawning
                currentWave = null;
            }
        }
    }

    private IEnumerator WaitThenNextWave(float wait)
    {
        // evitar reentradas
        if (waitingBetweenWaves) yield break;
        waitingBetweenWaves = true;
        float elapsed = 0f;
        while (elapsed < wait)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        waitingBetweenWaves = false;
        // Cargamos siguiente oleada
        LoadNextWaveFromQueue();
    }

    private void SpawnCapsule()
    {
        if (Mobs == null || Mobs.Length == 0 || spawnPoint == null) return;

        // Elige un mob aleatorio del array y un spawnpoint aleatorio tambien
        int idx = Random.Range(0, Mobs.Length);
        GameObject prefab = Mobs[idx];
        if (prefab == null) return;
        int randomIndex = Random.Range(0, spawnPoint.Length);
        GameObject temp=Instantiate(prefab, spawnPoint[randomIndex].position, spawnPoint[randomIndex].rotation);
        randomIndex = Random.Range(0, destination.Length);
        temp.GetComponent<Robot1Behaviour>().destination = destination[randomIndex];

    }

    // Genera 'n' oleadas aleatorias y las encola (cada oleada tendrá entre 3 y 4 capsulas)
    public void CreateRandomWaves(int n)
    {
        wavesQueue.Clear();
        for (int i = 0; i < n; i++)
        {
            int count = Random.Range(3, 5); // 3 o 4 (exclusive upper bound -> 3..4)
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            wavesQueue.Enqueue(new Wave(count, delay));
        }
    }

    // Inicia el proceso de oleadas: carga la primera y permite spawn en Update
    public void StartWaves()
    {
        if (wavesQueue.Count == 0) return;
        LoadNextWaveFromQueue();
        timer = 0f;
        spawnedInCurrentWave = 0;
    }

    // Detiene el proceso (no borra la cola)
    public void StopWaves()
    {
        currentWave = null;
        spawnedInCurrentWave = 0;
        timer = 0f;
        waitingBetweenWaves = false;
    }

    private void LoadNextWaveFromQueue()
    {
        if (wavesQueue.Count == 0)
        {
            currentWave = null;
            return;
        }

        currentWave = wavesQueue.Dequeue();
        spawnedInCurrentWave = 0;
        timer = 0f;
    }

    // Permite agregar manualmente una oleada (útil para pruebas)
    public void EnqueueWave(int count, float spawnDelay)
    {
        wavesQueue.Enqueue(new Wave(count, spawnDelay));
    }

    // Reinicia y crea nuevas oleadas aleatorias
    public void ResetAndCreateWaves(int n)
    {
        StopWaves();
        CreateRandomWaves(n);
        StartWaves();
    }

    // Opcional: público para saber si hay oleadas pendientes
    public bool HasPendingWaves() => wavesQueue.Count > 0 || currentWave != null;
    public int howManyPendingWaves()=>wavesQueue.Count;
}
