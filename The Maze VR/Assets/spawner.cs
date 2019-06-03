using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawner : MonoBehaviour
{
    public GameObject Player; //player GameoBject !!not prefab!! 
    // so the ai can follow him
    public GameObject niceAI; //niceAI prefab (add it in unity)
    public void spawn(float x, float z) //y is alwyas 0
    {
        var position = new Vector3(x, 0, z);
        var ai = Instantiate(niceAI, position, Quaternion.identity) as GameObject;
        ai.GetComponent<AIMove>().target = Player;
        ai.GetComponent<NavMeshAgent>().speed = 7; //speed
        ai.GetComponent<AIMove>().life = 25; //life time
        ai.GetComponent<animations>().Player = Player; //target
    }
}
