using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    [Tooltip("Minimum distance sholud ai reach to target")]
    public float Min_Distance;
    public float Flee_Out_Radius;
    public float Danger_Radius;


    [HideInInspector] public GameObject Player;
    [HideInInspector] public Waypoints_Manager WPM;
    public Enemy_Status ES;
    [HideInInspector] public Vector3 danger;
    public float distance;

    //Range of flee should work
    //otherwise Reset Agent 
    public float Max_range;

    public NavMeshAgent Agent;
    public NavMeshAgent Agent;
    public float Starting_Speed,Max_Speed;



    /// <summary>
    /// To reset the behaver of Agent to normal
    /// </summary>
    public void ResetAgent()
    {
        Agent.speed = Starting_Speed;
        Agent.angularSpeed = 200;
        //EAC.Animation_C("Walk","");
        Agent.ResetPath();

        Agent.SetDestination(WPM.WayPoints[Random.Range(0, WPM.WayPoints.Length)].transform.position);
    }


    /// <summary>
    /// Flee Function of Agent when ever player Fier in public
    /// </summary>
    /// <param name="From"></param>
    public void Flee(Vector3 From)
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < Danger_Radius)
        {
            Vector3 Flee_Dircetion = (transform.position - From).normalized;
            Vector3 New_Goal = transform.position + Flee_Dircetion * Flee_Out_Radius;

            NavMeshPath nav = new NavMeshPath();
            Agent.CalculatePath(New_Goal, nav);

            if (nav.status != NavMeshPathStatus.PathInvalid)
            {
                danger = From;
                Agent.SetDestination(nav.corners[nav.corners.Length - 1]);
                Agent.speed = Max_Speed;
                Agent.angularSpeed = Random.Range(200, 500);
            }
            Invoke("ResetAgent", 10f);
        }
    }


    private void Start()
    {
        ES = GetComponent<Enemy_Status>();


        Agent = GetComponent<NavMeshAgent>();

        Agent.SetDestination(WPM.WayPoints[Random.Range(0, WPM.WayPoints.Length)].transform.position);
        //EAC = GetComponentInChildren<Enemy_Animation_Controller>();
        //ERC = GetComponentInChildren<Enemy_RagDoll_Controle>();
    }
    
    
    void Update()
    {
        distance =  Vector3.Distance(transform.position, Player.transform.position);
        if (Vector3.Distance(transform.position, Player.transform.position) > Max_range)
        {
            Destroy(gameObject);
            WPM.alive_Agents--;
            WPM.Spawn_NPC();
        }
        else
        {
            if (Vector3.Distance(transform.position, Agent.destination) < Min_Distance)
            {
                Agent.SetDestination(WPM.WayPoints[Random.Range(0, WPM.WayPoints.Length)].transform.position);

            }
        }

        
    }
}
