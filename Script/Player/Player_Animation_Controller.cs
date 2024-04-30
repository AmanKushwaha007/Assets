using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour
{

    [System.Serializable]
    public class CamControle
    {
        public string Name;
        public GameObject Cinemachine;
    }
    public List<CamControle> Cinemachines;


    Player_Ragdoll_Cotrole PRC;
    [HideInInspector] public Player_Movement PM;


    public void Change()
    {

    }
    
    
    public void Entery()
    {
        PM.Player_As_Agent.enabled = true;
        PM.Player_As_Agent.destination = PM.PVH.Destination.transform.position;
    }
   
    
    public void ChangeShoulderLocation()
    {
        if (!PRC.IsDead)
        {
            if (PM.isCrouching)
            {
                if (PM.isAiming) Change_Cinemachine("Aim Crouch");
                else Change_Cinemachine("Without Aim Crouch");
            }
            else if(!PM.isCrouching)
            {
                if (PM.isAiming) Change_Cinemachine("Aim");
                else Change_Cinemachine("Without Aim");
            }
        }
        else
        {
            Debug.Log(PRC.IsDead);
            Change_Cinemachine("Dead");
        }
    }

    
    /// <summary>
    /// Change Cinemachine to another Cinemachine
    /// Without Aim
    /// Aim
    /// Without Aim Crouch
    /// Aim Crouch
    /// Dead
    /// </summary>
    /// <param name="Cinemachine_Name"></param>
    public void Change_Cinemachine(string Cinemachine_Name)
    {
        for (int i = 0; i < Cinemachines.Capacity; i++)
        {
            if (Cinemachines[i].Name == Cinemachine_Name)
                if(Cinemachines[i].Cinemachine.activeSelf == true)
                {
                    break;
                }
                else
                {
                    foreach (var item in Cinemachines)
                    {
                        item.Cinemachine.SetActive(false);
                    }
                    for (int j = 0; j < Cinemachines.Capacity; j++)
                    {
                        if (Cinemachines[j].Name == Cinemachine_Name) Cinemachines[j].Cinemachine.SetActive(true);
                    }
                }
        }
    }
}
