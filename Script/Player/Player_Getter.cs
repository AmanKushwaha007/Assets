using UnityEngine;

public class Player_Getter : MonoBehaviour
{
    [HideInInspector] public Player_Weapon PW;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            other.GetComponent<Weapon_Script>().Assign_Weapon_Details(PW);
        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            PW.Current_Collided_Wepon_Details = new Weapon_Script.Weapon_Details();
        }
    }
}
