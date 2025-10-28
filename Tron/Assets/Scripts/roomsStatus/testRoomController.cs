using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRoomController : MonoBehaviour
{
    int id = 0;
    float timer=0;
    bool emergency;
    bool isSpawned;

    public Renderer renderLampColor;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        isSpawned = eventManager.Instance.GetEvent(id).isSpawned;
        emergency = eventManager.Instance.GetEvent(id).eventEmergency;

        if (isSpawned&&!emergency)
        {
            timer += Time.deltaTime;
        }
        else timer = 0; 

        if (timer >= 10 && isSpawned)
        {
            eventManager.Instance.GetEvent(id).eventEmergency = true;
            eventManager.Instance.eventEmergiCount++;
            Debug.Log("Emergencia");
        }
        if (renderLampColor != null && emergency)
        {
            renderLampColor.material.color = Color.red; // crea/usa una instancia sólo para ese renderer
        }
        else renderLampColor.material.color = Color.white;
    }
}
