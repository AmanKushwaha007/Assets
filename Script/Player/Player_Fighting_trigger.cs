using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fighting_trigger : MonoBehaviour
{
    public Enemy_Status ES;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy") 
        {
            ES = other.GetComponent<Enemy_Status>();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            ES = null;
        }
    }
}
