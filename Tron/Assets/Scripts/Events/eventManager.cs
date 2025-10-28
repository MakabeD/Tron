using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public static eventManager Instance;

    public int eventCount;
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
        eventSpawner.Instance.setEvets(new Event[] { new testEvent1(1) });
        eventCount = 0;
    }
    public Event GetEvent(int x)
    {
        return eventSpawner.Instance.getEvets()[x];
    }

    // Update is called once per frame
    void Update()
    {
        if (eventCount > 0) { SuspiciousActivity.Instance.activateAdvertisement(); Debug.Log("ActivandoAviso"); return; }
        SuspiciousActivity.Instance.deactivateAdvertisement();
    }
}
