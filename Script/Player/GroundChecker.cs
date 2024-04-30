using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool countTrigger = false;


    private void OnTriggerEnter(Collider other)
    {
        countTrigger = true;
    }


    private void OnTriggerStay(Collider other)
    {
        countTrigger = true;
    }


    private void OnTriggerExit(Collider other)
    {
        countTrigger = false;
    }
}
