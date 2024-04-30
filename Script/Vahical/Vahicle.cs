using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Vahicle : MonoBehaviour
{
    [System.Serializable]
    public struct Vahicle_Details
    {
        //public GameObject Vahicle_prefab;
        public float Motor_Speed, Break_froce, Sensitivity, Mass, Max_Vahical_Speed;

        public bool is_Player_Inside;
        public Transform[] Vahicle_Wheel_Transform;
        public WheelCollider[] Vahicle_Wheel_Collider;
    }
    public Vahicle_Details vahicle_Details;
    public GameObject Vahical_Cinemachine;    

    [HideInInspector] public bool IsPlayerIn;
    [HideInInspector] public Player_Vahical_Handler PVH;
    public Exit_Location EL;
    
    public bool isBreakApplied;
    [Header("Visual Wheel transform should take wheel collider position")]
    public bool isTranform;


    public float Input_Horizontal, Input_Vertical, CurrnetMotorSpeed;
    public Vahical_Status VS;

    /*
    void Start()
    {
    }
    
     public void Update()
    {
        Input_Horizontal = Input.GetAxis("Horizontal");
        Input_Vertical = Input.GetAxis("Vertical");

            // If i remove that update to fixed then it will update at 0.02 seconds.
            // that's why Input are here


            UpdateWheelToWheelCollider(vahicle_Details.Vahicle_Wheel_Collider,
                                        vahicle_Details.Vahicle_Wheel_Transform);
    }
    public void FixedUpdate()
    {
            Break_force();
            Motor_force();
            Rotate_Wheel();
    }
    */
    /// <summary>
    /// For Rotate vahical front wheels
    /// </summary>
    /// <param name="Input_Horizontal"></param>
    public void Rotate_Wheel()
    {
        float SteerAngle = Input_Horizontal * vahicle_Details.Sensitivity;
        vahicle_Details.Vahicle_Wheel_Collider[0].steerAngle = SteerAngle;
        vahicle_Details.Vahicle_Wheel_Collider[1].steerAngle = SteerAngle;
    }

    float currentBreakForce;
    /// <summary>
    /// For aply break in vahical
    /// </summary>
    public void Break_force()
    {
        currentBreakForce = Input.GetKey(KeyCode.Space) ? vahicle_Details.Break_froce : 0;
        Debug.Log(currentBreakForce);
        vahicle_Details.Vahicle_Wheel_Collider[0].brakeTorque = currentBreakForce;
        vahicle_Details.Vahicle_Wheel_Collider[1].brakeTorque = currentBreakForce;
        vahicle_Details.Vahicle_Wheel_Collider[2].brakeTorque = currentBreakForce;
        vahicle_Details.Vahicle_Wheel_Collider[3].brakeTorque = currentBreakForce;
    }
    /// <summary>
    /// For aply motortorque in vahical
    /// </summary>
    /// <param name="Input_Vertical"></param>
    public void Motor_force()
    {
        CurrnetMotorSpeed = vahicle_Details.Motor_Speed * Input_Vertical;
        // For makeing vahical under certain speed 
        // or
        // Making speed limit of vahical
        
        if (VS.Speed > vahicle_Details.Max_Vahical_Speed)
        {
            CurrnetMotorSpeed = 0;
        }

        vahicle_Details.Vahicle_Wheel_Collider[2].motorTorque = CurrnetMotorSpeed;
        vahicle_Details.Vahicle_Wheel_Collider[3].motorTorque = CurrnetMotorSpeed;
    }
    /// <summary>
    /// For aplying wheel tranform and rotaion from wheel collider to visual wheel
    /// </summary>
    /// <param name="WC"></param>
    /// <param name="WT"></param>
    public void UpdateWheelToWheelCollider(WheelCollider[] WC,Transform[] WT)
    {
        // looping and aplying 
        Aply(WC[0], WT[0]);
        Aply(WC[1], WT[1]);
        Aply(WC[2], WT[2]);
        Aply(WC[3], WT[3]);

    }
    /// <summary>
    /// Realone is here which will apply
    /// </summary>
    /// <param name="WC"></param>
    /// <param name="WT"></param>
    void Aply(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;
        WC.GetWorldPose(out position, out rotation);
        WT.transform.rotation = rotation;
        if (isTranform)
            WT.transform.position = position;
    }


}