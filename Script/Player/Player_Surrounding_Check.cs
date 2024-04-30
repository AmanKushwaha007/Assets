using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Surrounding_Check : MonoBehaviour
{
    [HideInInspector] public Player_Movement PM;
    public GameObject Last_Vahical;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Vahical")
        {
            PM.PVH.Available_Vahical = other.GetComponent<Vahicle>();
            Last_Vahical = other.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Last_Vahical)
        {
            PM.PVH.Available_Vahical = null;
            Last_Vahical = other.gameObject;
        }
    }

}
