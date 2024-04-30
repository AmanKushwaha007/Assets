using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status : MonoBehaviour
{
    public float health, stamina;
    public Slider Health_Bar;
    public Slider Streanth_Bar;


    /// <summary>
    /// It will take Damage ( Amount ) and If Health
    /// is lower or equal to 0
    /// then player will die
    /// </summary>
    /// <param name="X"></param>
    public void Take_damage(float X)
    {
        health -= X;

        if(health <= 0)
        {
            health = 0;
            Health_Bar.value = 0;
            Die();
        }
    }


    public void Die()
    {

    }
}
