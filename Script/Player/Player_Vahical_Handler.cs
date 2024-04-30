using UnityEngine;

public class Player_Vahical_Handler : MonoBehaviour
{
    public Vahicle Current_Vahical;
    public Vahicle Available_Vahical;
    public bool Break, enterCalled, exitCalled;
    [HideInInspector] public Player_Movement PM;
    public GameObject Destination;

    public void VahicalControle()
    {
        if (Current_Vahical != null && Current_Vahical.IsPlayerIn)
        {

            // If i remove that update to fixed then it will update at 0.02 seconds.
            // that's why Input are here
            Current_Vahical.Input_Horizontal = PM.Input_Horizontal;
            Current_Vahical.Input_Vertical = PM.Input_Vertical;


            Current_Vahical.UpdateWheelToWheelCollider(Current_Vahical.vahicle_Details.Vahicle_Wheel_Collider,
                                        Current_Vahical.vahicle_Details.Vahicle_Wheel_Transform);
            if (Input.GetKeyDown(KeyCode.G))
            {
                Exit();
            }
        }

        if (Available_Vahical != null)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Current_Vahical = Available_Vahical;
                if (Current_Vahical.EL.isSpaceAvailable())
                {
                    Destination = Current_Vahical.EL.SpaceObject;
                    PM.rg.isKinematic = true;
                    PM.rg.useGravity = false;
                    enterCalled = true;
                    PM.PAC.Entery();
                }         
            }
        }
    }


    public void Vahical_Physics()
    {
        if (Current_Vahical != null && Current_Vahical.IsPlayerIn)
        {
            Current_Vahical.Break_force();
            Current_Vahical.Motor_force();
            Current_Vahical.Rotate_Wheel();
        }
    }


    /// <summary>
    /// It is call of player when it wants to get in vahical
    /// </summary>
    public void Enter()
    {
        enterCalled = false;
        AssigningValue(true);
        Available_Vahical = null;
    }
    
    
    /// <summary>
    /// For Applying values to vahical, as per requirment
    /// </summary>
    /// <param name="vahical"></param>
    /// <param name="isTrue"></param>
    public void Exit()
    {
        if (Current_Vahical.EL.isSpaceAvailable())
        {
            PM.Player_As_Agent.enabled = false;
            gameObject.transform.position = Current_Vahical.EL.SpaceObject.transform.position;
            gameObject.transform.rotation = Quaternion.Euler(0,Current_Vahical.EL.SpaceObject.transform.eulerAngles.y,0);
            AssigningValue(false);
        }
        else Debug.Log("No place to GetOut of Vahical");

    }
    
    
    private void AssigningValue(bool isTrue)
    {
        if (isTrue)
        {
            Current_Vahical.PVH = this;
            Current_Vahical.IsPlayerIn = true;
            Current_Vahical.isBreakApplied = true; 
            ChangeDriver(true);
        }
        else
        {
            Current_Vahical.PVH = null;
            Current_Vahical.IsPlayerIn = false;
            ChangeDriver(false);
            Current_Vahical = null;
        }
    }

    
    private void ChangeDriver(bool isTrue)
    {
        foreach (var child in PM.Children)
        {
            child.SetActive(!isTrue);
        }
        PM.rg.isKinematic = isTrue;
        PM.rg.useGravity = !isTrue;
        PM.playerCollider.enabled = !isTrue;
        PM.PlayerCine.SetActive(!isTrue);
        Current_Vahical.Vahical_Cinemachine.SetActive(isTrue);
    }
}
