using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Status : MonoBehaviour
{
    public float Max_Health;
    Waypoints_Manager WPM;
    private void Start()
    {
        //WPM = GameObject.FindGameObjectWithTag("WPM").GetComponent<Waypoints_Manager>();
    }
    /// <summary>
    /// It take Damage( Amount ) from weapon
    /// and
    /// if Health is lower or equal to Zero
    /// then RagDoll Have call
    /// </summary>
    /// <param name="X"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Vahical"))
        {
            if(collision.collider.GetComponent<Vahical_Status>().Speed > 10)
            {
                Take_damage(110);
            }
        }
    }
    
    
    public void Take_damage(float X)
    {
        Max_Health -= X;
        if (Max_Health <= 0)
        {
            GetComponent<Enemy_AI>().enabled = false;
            GetComponentInChildren<Enemy_RagDoll_Controle>().IsDead = true;
        }
    }
}
