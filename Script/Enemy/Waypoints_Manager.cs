using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints_Manager : MonoBehaviour
{
    public GameObject[] WayPoints;
    [Tooltip("This takes angent types as gameobject into itself.")]
    public GameObject[] Agnets;

    public GameObject Player;

    public int Handleable_max_Agent_Count, alive_Agents, max_Agent_Count, collider_Called_Max_Agent_Count;

    public List<GameObject> newWaypoints;

    //Range of flee should work
    //otherwise Reset Agent 
    public float Max_Range_NPC;

    public int i = 0;


    private void Start()
    {
        alive_Agents = 0;
        newWaypoints = new List<GameObject>();
        Start_Spawn_NPC(max_Agent_Count);
        collider_Called_Max_Agent_Count = max_Agent_Count;

    }


    public void CreateNewWayPoints()
    {
        newWaypoints.Clear();
        for (int i = 0; i < WayPoints.Length; i++)
        {
            if (Vector3.Distance(Player.transform.position, WayPoints[i].transform.position) < Max_Range_NPC)
            {
                newWaypoints.Add(WayPoints[i]);
            }
        }
    }


    public void InstanceEnemy()
    {
        if (newWaypoints.Count == 0) return;
        alive_Agents++;
        Transform Location = newWaypoints[Random.Range(0, newWaypoints.Count)].transform;
        GameObject agent = Instantiate(Agnets[0], Location.position, Location.rotation);
        Enemy_AI enemy = agent.GetComponent<Enemy_AI>();
        enemy.Player = Player;
        enemy.WPM = this;
        enemy.Max_range = Max_Range_NPC;
    }


    public void Start_Spawn_NPC(int count)
    {
        collider_Called_Max_Agent_Count = count;
        CreateNewWayPoints();
        StartCoroutine(StartingInstancer());
    }


    IEnumerator StartingInstancer()
    {
        if (newWaypoints.Count <= 0)
            yield return null;


        while (i < collider_Called_Max_Agent_Count)
        {
            if (alive_Agents >= Handleable_max_Agent_Count)
                yield break;

            InstanceEnemy();
            InstanceEnemy();
            i += 2;
            yield return null;
        }
        i = 0;
    }


    public void Spawn_NPC()
    {
        CreateNewWayPoints();

        StartCoroutine(Instancer());
    }


    IEnumerator Instancer()
    {
        if (newWaypoints.Count <= 0)
            yield return null;
        InstanceEnemy();
    }
}
