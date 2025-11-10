using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timerController : MonoBehaviour
{
    public TextMeshProUGUI textMeshTimer;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = TestRoomController.Instance.GetEmergencyTimer();
        textMeshTimer.text = string.Format(">>segundos restantes: {0:00}<<", 60-timer);
    }
}
