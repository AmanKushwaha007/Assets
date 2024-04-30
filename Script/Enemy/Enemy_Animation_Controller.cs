using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Animation_Controller : MonoBehaviour
{
    Animator Enemy_Animator;
    [Range(0, 3)]
    public float Horizontal;
    [Range(0,3)]
    public float Vertical;
    public Enemy_AI EA;

    public bool isJumped;
    public bool isCrouching;
    public bool isHoldingWeapon;
    public bool isAiming;
    private void Start()
    {
        Enemy_Animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Animation_Parameters();
    }
    /// <summary>
    /// Apply Which Animation you want's to Agent should perform
    /// Walk
    /// Running
    /// Crouch
    /// These are those right now
    /// And it should be in string
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Animation_C(string x,string y)
    {
        if (x == "Walk") Vertical = 1;
        else if (x == "Running") Vertical = 2.9f;
        if (y == "Crouch") isCrouching = true;
    }
    private void Animation_Parameters()
    {
        //Enemy_Animator.SetBool("Moveing", EA.Agent.velocity > 2);
        Enemy_Animator.SetBool("Jump", isJumped);
        Enemy_Animator.SetBool("IsCrouching", isCrouching);
        Enemy_Animator.SetBool("IsGunAvailable", isHoldingWeapon);
    }
}
