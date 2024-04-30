using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Landing_Space : MonoBehaviour
{
    public int triggerCount;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        triggerCount++;
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            triggerCount--;
    }
}
