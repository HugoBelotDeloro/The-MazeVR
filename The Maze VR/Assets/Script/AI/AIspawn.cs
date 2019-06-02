using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIspawn : MonoBehaviour
{
    public GameObject AI;
    public GameObject Player;
    private float timer; 
    public float starttime; //ai spawn every x seconds
    public float startspeed; //ai speed
    public float startlife; //ai life time (seconds)
    public float incr_timer; //increase coefs - should be lower than 1 (to increase spawn rate)
    public float incr_speed;// -- should be higher than 1 (to increase speed)
    public float incr_life;// --  same / to increase life time

    void Start()
    {
        timer = starttime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {   
            timer = starttime; //reset timer
            starttime *= incr_timer; //decrease
            if (GameObject.Find("AI(Clone)") == null)
            {
                spawn(20, 40);
                Debug.Log("AI spawned!");
            }
            else
            {
                Debug.Log("There already is an alive AI");
            }

        }
    }

    void spawn(float minrange, float startrange)
    {
        Vector3 position;
        if (Random.Range(0, 10) > 5)
        {
            position = new Vector3(Player.transform.position.x + Random.Range(minrange, startrange), 0, Player.transform.position.z + Random.Range(minrange, startrange));
        }
        else
        {
            position = new Vector3(Player.transform.position.x + Random.Range(-minrange, -startrange), 0, Player.transform.position.z + Random.Range(-minrange, -startrange));
        }
        var ai = Instantiate(AI, position, Quaternion.identity) as GameObject;
        ai.GetComponent<AIMove>().target = Player;
        ai.GetComponent<NavMeshAgent>().speed = startspeed;
        ai.GetComponent<AIMove>().life = startlife;
        startspeed *= incr_speed;
        startlife *= incr_life;
    }
}
