using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Ragdoll_Cotrole : MonoBehaviour
{
    [System.Serializable]
    public class Ragdol
    {
        public Collider[] colliders;
        public Rigidbody[] rigidbodies;

    }   
    public Ragdol ragdoll;
    public Player_Animation_Controller PAC;


    [HideInInspector] public Player_Movement PM;


    public float Dead_Cam_Timeing;
    public GameObject Main_Cam;


    public bool IsDead;


    private void Start()
    {
        PAC = GetComponent<Player_Animation_Controller>();
        IsDead = false;
        ragdoll.colliders = GetComponentsInChildren<Collider>();
        ragdoll.rigidbodies = GetComponentsInChildren<Rigidbody>();

        //Ensure that collider and rigidody is not working 
        // and it should work as we want's.
        foreach (var colliders in ragdoll.colliders)
        {
            colliders.enabled = false;
        }
        foreach (var item in ragdoll.rigidbodies)
        {
            item.isKinematic = true;
            item.useGravity = false;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown("h")) IsDead = true;
        if(IsDead)
        {
            AfterDead();
        }
    }


    /// <summary>
    /// After Dead call, it will do as any it should
    /// </summary>
    private void AfterDead()
    {
        Main_Cam.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = Dead_Cam_Timeing;
        gameObject.transform.parent = null;
        IsDead = true;
        PM.playerCollider.enabled = false;
        PM.rg.useGravity = false;
        PM.rg.isKinematic = false;
        foreach (var colliders in ragdoll.colliders)
        {
            colliders.enabled = true;
        }
        foreach (var item in ragdoll.rigidbodies)
        {
            item.isKinematic = false;
            item.useGravity = true;
        }
    }
}
