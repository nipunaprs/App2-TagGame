using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    //Variables
    public GameObject player;
    private NavMeshAgent agent;
    public Animator playerAnimator;
    public bool isIt = false;

    //Running away variables
    public float enemyProximityDistance = 10.0f;
    public float enemyProximityRunAwayDistance = 30.0f;


    //Throwing
    public Transform attackPoint;
    public GameObject rock;
    public float throwForce;
    public float throwUpwardForce;
    public float throwCooldown=4f;
    public bool readyToThrow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        readyToThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isIt)
        {
            //Run towards player
            agent.destination = player.transform.position;
            agent.updateRotation = true;

            if (readyToThrow && isIt)
            {
                ThrowRocks();
            }

            
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //find distance btwn enemy and player

            if(distance < enemyProximityRunAwayDistance)
            {
                Vector3 dirToPlayer = transform.position - player.transform.position; //Find direction to player
                Vector3 newPos = transform.position + dirToPlayer; //Set new position to itself + that

                agent.SetDestination(newPos);
            }
        }





        if(agent.velocity.magnitude > 1)
        {
            //Handle running animations
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("runStraight", true);
        }
        else
        {
            playerAnimator.SetBool("runStraight", false);
            playerAnimator.SetBool("idle", true);
        }
    }

    void ThrowRocks()
    {

        

        float distance = Vector3.Distance(transform.position, player.transform.position); //find distance btwn enemy and player

        if (distance < enemyProximityDistance)
        {
            readyToThrow = false;

            Vector3 dirToPlayer = transform.position - player.transform.position;

            //Make object to throw
            GameObject throwobj = Instantiate(rock, attackPoint.position, transform.rotation);

            //Grab the rgbody of the projectile
            Rigidbody throwobjRB = throwobj.GetComponent<Rigidbody>();

            Vector3 forceAdding = transform.forward * throwForce + transform.up * throwUpwardForce;

            throwobjRB.AddForce(forceAdding, ForceMode.Impulse); // impulse b/c only adding force once not contiuously

            Invoke(nameof(ResetThrow), throwCooldown);
        }
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rock" && isIt == false)
        {
            isIt = true;
            player.GetComponent<PlayerMovement>().isIt = false;
            Debug.Log("Enemy hit -- You are no longer it -- Run away");
            Destroy(collision.gameObject); //destroy the rock
        }
    }



}
