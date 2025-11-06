using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomController : MonoBehaviour
{
    int id = 0;
    public float timer = 0;
    bool emergency;
    bool isSpawned;
    public Renderer renderLampColor;
    public static TestRoomController Instance;
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
        isSpawned = EventManager.Instance.GetEvent(id).IsSpawned;
        emergency = EventManager.Instance.GetEvent(id).EventEmergency;
        if (renderLampColor != null && emergency)
        {
            renderLampColor.material.color = Color.red; // crea/usa una instancia sólo para ese renderer
        }
        else if(renderLampColor != null && isSpawned)
        {
            renderLampColor.material.color = Color.yellow;
        }
        else renderLampColor.material.color = Color.white;
        if (isSpawned && !emergency)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            return;
        }

        if (timer >= 60 && !EventManager.Instance.GetEvent(id).EventEmergency)
        {
            GameHP.Instance.GetDownLife(10);
            EventManager.Instance.GetEvent(id).EventEmergency = true;
            GameManager.Instance.isinCombat++;

            EventManager.Instance.IncrementEmergencyEventCount();
            Debug.Log("emergencia por tiempo(database)");
        }
        
    }
    public float getEmergencyTimer()
    {
        if (!emergency) return timer;
        return 60;
    }
}
