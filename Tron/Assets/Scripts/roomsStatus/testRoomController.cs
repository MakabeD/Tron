using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRoomController : MonoBehaviour
{
    int id = 0;
    public float timer=0;
    bool emergency;
    bool isSpawned;
    public Renderer renderLampColor;
    public static testRoomController Instance;
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

        if (isSpawned&&!emergency)
        {
            timer += Time.deltaTime;
        }
        else timer = 0; 

        if (timer >= 50 && isSpawned)
        {
            EventManager.Instance.GetEvent(id).EventEmergency = true;
            EventManager.Instance.IncrementEmergencyEventCount();
            Debug.Log("Emergencia");
        }
        if (renderLampColor != null && emergency)
        {
            renderLampColor.material.color = Color.red; // crea/usa una instancia sólo para ese renderer
        }
        else renderLampColor.material.color = Color.white;
    }
    public  float getEmergencyTimer()
    {
        if(!emergency)return timer;
        return 50;
    }
}
