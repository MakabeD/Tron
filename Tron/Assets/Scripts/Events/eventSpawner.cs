using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class eventSpawner : MonoBehaviour
{
    public static eventSpawner Instance;
    public bool spawnActivate=false;
    public Event[] events = new Event[6];
    float counter = 0f;


    public void setEvets(Event[] evetss) { events = evetss; }
    public Event[] getEvets() { return events; }

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
        if (!spawnActivate) return;
        
        counter += Time.deltaTime;
        if (counter >= 15f)
        {
            roulette();
            counter = 0f;
        }
    }

    void roulette()
    {
        for (int i = 0; i < events.Length; i++)
        {
            Debug.Log("buscando...");
            if (UnityEngine.Random.value < events[i].getProbability() && !events[i].isSpawned) //agregar .chance
            {
                Debug.Log("encontrado"+ events[i].getProbability());
                events[i].ExcecuteEvent();
                events[i].getDownProbability(events[i].getProbability()/2f);
                events[i].isSpawned = true;
                refill(events[i].getProbability() / events.Length, i);
                //spawnea y reduce la probabilidada; aumenta las otras
                return;
            }
        }
    }
    void refill(float i, int index)
    {
        for (int j=0;j<events.Length; j++)
        {
            if (index != j) events[j].getUpProbability(i);
        }
    }
}


public abstract class Event
{
    
    public float probability = 0f;
    public bool isSpawned = false;
    public bool eventEmergency = false;


    

    public abstract void ExcecuteEvent();
    public abstract void stopEventExcecute();

    public float getProbability() { return probability; }

    public void getDownProbability(float x)
    {
        probability -= x;
    }

    public void getUpProbability(float x)
    {
        probability += x;
    }


}