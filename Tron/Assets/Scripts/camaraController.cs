using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraController : MonoBehaviour
{
    public float velocity = 100f;
    float rotationX = 0f;
    
    public Transform player;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * velocity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * velocity * Time.deltaTime;


        if (GameManager.Instance.isInGame) return; 
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        player.Rotate(Vector3.up * mouseX);
        
    }
}
