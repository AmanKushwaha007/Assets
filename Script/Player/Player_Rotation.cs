using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    public GameObject Player_Shoulder;
    public float Sensitivity, MaxUpAngle, MaxDownAngle;
    public Vector3 Rotation;

    float Input_Horizontal, Input_Vertical;


    public void player_MouseInput()
    {
        Input_Horizontal = Input.GetAxis("Mouse X");
        Input_Vertical = Input.GetAxis("Mouse Y");
    }


    public void player_Rotaion_and_CamRotation()
    {
        transform.localEulerAngles += new Vector3(0, Input_Horizontal * Sensitivity, 0);
        Rotation += new Vector3(Input_Vertical * Sensitivity, 0, 0);

        if (Rotation.x > MaxUpAngle && Rotation.x < 360 && Rotation.x < 180)
            Rotation = new Vector3(MaxUpAngle, Rotation.y, 0);
        if (Rotation.x < MaxDownAngle && Rotation.x > -360 && Rotation.x > -180)
            Rotation = new Vector3(MaxDownAngle, Rotation.y, 0);

        Player_Shoulder.transform.localEulerAngles = Rotation;
    }
}
