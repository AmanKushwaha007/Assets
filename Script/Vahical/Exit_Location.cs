using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Location : MonoBehaviour
{
    public GameObject SpaceObject;
    public Check_Landing_Space[] CLS;


    public bool isSpaceAvailable()
    {
        for (int i = 0; i < CLS.Length; i++)
        {
            if(CLS[i].triggerCount==0)
            {
                SpaceObject = CLS[i].gameObject;
                return true;
            }
        }
        return false;
    }
}
