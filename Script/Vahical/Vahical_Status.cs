using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vahical_Status : MonoBehaviour
{
    public float Health, Max_Speed, Max_Health;
    public GameObject[] Unattachable;
    public Vahicle vahical;
    public bool Should_It_Destroy;

    public Text Speed_Text;
    public Slider Health_Slider;

    [HideInInspector] public float Timer_To_Alive;
    [HideInInspector] public bool isInMPH;
    [HideInInspector] public float Speed;


    Rigidbody RB;
    float Speed_Is_In;


    public void Start()
    {
        RB = GetComponent<Rigidbody>();
        vahical = GetComponent<Vahicle>();


        Speed_Is_In = 3.6f;
        if (isInMPH) Speed_Is_In = 2.237f;
        if (Timer_To_Alive <= 1) Timer_To_Alive = 7;

    }


    /// <summary>
    /// It take Damage( Amount ) from wepon and if health is lower
    /// then Zero, then vahical will destroy
    /// with some time which can be change as per needed
    /// </summary>
    /// <param name="Damage"></param>
    public void Takedamage(float Damage)
    {
        Health -= Damage;
        if(Health <= 0)
        {
            vahical.enabled = false;
            foreach (var item in Unattachable)
            {
                item.gameObject.transform.parent = null;
                item.GetComponent<Unattachable>().unattachedNow(Timer_To_Alive);
            }
            if(Should_It_Destroy)Invoke("detroyGameObject", Timer_To_Alive);
        }
    }
    
    
    /// <summary>
    /// For Destroy vahical Instead
    /// </summary>
    public void detroyGameObject()
    {
        Destroy(gameObject);
    }


    // For drift add Extremum silp
}
