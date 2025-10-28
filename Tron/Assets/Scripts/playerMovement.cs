using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float velocity=15f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundCheckRadius=0.3f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 gVelocity;
    //nueva variable para lockear el movimiento
    public bool canMove=true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded && gVelocity.y <0)
        {
            gVelocity.y = -2;
        }

        //integrando efectos de la simulacion
        if (!canMove) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        gVelocity.y += gravity * Time.deltaTime;

        Vector3 move = transform.right * x+transform.forward * z;
        controller.Move(gVelocity * Time.deltaTime);
        controller.Move(move*velocity* Time.deltaTime);
    }
}
