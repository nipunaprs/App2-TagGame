using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {
            
            
            Destroy(this.gameObject); //destroy the rock
        }
    }
}
