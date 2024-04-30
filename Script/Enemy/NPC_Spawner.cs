using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    public Waypoints_Manager WPM;
    public int Max_NPC_Counts;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") WPM.Start_Spawn_NPC(Max_NPC_Counts);
    }
}
