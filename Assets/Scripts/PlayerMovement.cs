using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables
    public CharacterController controller;
    public float speed = 12f;

    Vector3 fallvelocity;
    public float gravity = -9.81f;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity code
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && fallvelocity.y < 0)
        {
            fallvelocity.y = -2f;
        }




        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); //Move framerate independant

        fallvelocity.y += gravity * Time.deltaTime;
        controller.Move(fallvelocity * Time.deltaTime);

    }
}
