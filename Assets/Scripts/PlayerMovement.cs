using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables
    public CharacterController controller;
    public float speed = 12f;
    public Animator playerAnimator;

    //Gravity 
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

        //Animation code
        HandleAnimations();



        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); //Move framerate independant

        fallvelocity.y += gravity * Time.deltaTime;
        controller.Move(fallvelocity * Time.deltaTime);

    }

    void HandleAnimations()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            //Run straight
            playerAnimator.SetBool("runStraight", true);
            playerAnimator.SetBool("runLeft", false);
            playerAnimator.SetBool("runRight", false);
            playerAnimator.SetBool("runBack", false);
            playerAnimator.SetBool("idle", false);

        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)) && (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)))
        {
            //Run left
            playerAnimator.SetBool("runLeft", true);
            playerAnimator.SetBool("runStraight", false);
            playerAnimator.SetBool("runRight", false);
            playerAnimator.SetBool("runBack", false);
            playerAnimator.SetBool("idle", false);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)) && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)))
        {
            //Run right
            playerAnimator.SetBool("runRight", true);
            playerAnimator.SetBool("runLeft", false);
            playerAnimator.SetBool("runStraight", false);
            playerAnimator.SetBool("runBack", false);
            playerAnimator.SetBool("idle", false);

        }
        else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            //Run back
            playerAnimator.SetBool("runBack", true);
            playerAnimator.SetBool("runRight", false);
            playerAnimator.SetBool("runLeft", false);
            playerAnimator.SetBool("runStraight", false);
            playerAnimator.SetBool("idle", false);

        }
        else
        {
            //Run idle
            playerAnimator.SetBool("idle", true);
            playerAnimator.SetBool("runBack", false);
            playerAnimator.SetBool("runRight", false);
            playerAnimator.SetBool("runLeft", false);
            playerAnimator.SetBool("runStraight", false);

        }
    }
}

