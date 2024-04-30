using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RagDoll_Controle : MonoBehaviour
{
    public float Decomposing_Time;



    [System.Serializable]
    public class Ragdol
    {
        public Collider[] colliders;
        public Rigidbody[] rigidbodies;

    }
    [HideInInspector]
    public Ragdol ragdoll;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Collider Main_Player_Collider;
    [HideInInspector]
    public Rigidbody Main_Player_Rigidbody;
    [HideInInspector]
    public Waypoints_Manager WPM;
    [HideInInspector]
    public bool IsDead,End;

    private void Start()
    {
        End = false;
        WPM = GameObject.FindGameObjectWithTag("WPM").GetComponent<Waypoints_Manager>();
        animator = GetComponent<Animator>();
        Main_Player_Collider = GetComponentInParent<Collider>();
        Main_Player_Rigidbody = GetComponentInParent<Rigidbody>();
        IsDead = false;
        ragdoll.colliders = GetComponentsInChildren<Collider>();
        ragdoll.rigidbodies = GetComponentsInChildren<Rigidbody>();

        // For make sure Collider and Rigidboy is not working for Agent
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
        if (IsDead)
        {
            animator.enabled = false;
            gameObject.transform.parent = null;
            if (Main_Player_Collider != null && Main_Player_Collider.enabled == true)
            {
                Dead();
            }
            if (!End)
            {
                End = true;
                Invoke("ClearEnemy", Decomposing_Time);
            }

        }
    }
    /// <summary>
    /// It will force to only ragdoll remain
    /// </summary>
    private void Dead()
    {
        Destroy(Main_Player_Collider.gameObject);
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
    /// <summary>
    /// Destory Main NPC or Enmey Controller and don't Destroy Ragdoll
    /// </summary>
    private void ClearEnemy()
    {
        Destroy(gameObject);
    }
}
