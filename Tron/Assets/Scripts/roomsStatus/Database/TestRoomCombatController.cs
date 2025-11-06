using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomCombatController : MonoBehaviour
{   int id = 0;
    
     
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isinCombat >0 && EventManager.Instance.GetEvent(id).EventEmergency) 
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }
    }
}
