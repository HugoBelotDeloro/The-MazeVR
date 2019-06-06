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
        AI.GetComponent<AIMove>().target = Player;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {   
            timer = starttime; //reset timer
            starttime *= incr_timer; //decrease
            if (!AI.activeSelf)
            {
                spawn(50, 70);
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
            position = new Vector3(Player.transform.position.x + Random.Range(minrange, startrange), (float) transform.position.y, Player.transform.position.z + Random.Range(minrange, startrange));
        }
        else
        {
            position = new Vector3(Player.transform.position.x + Random.Range(-minrange, -startrange), (float)transform.position.y, Player.transform.position.z + Random.Range(-minrange, -startrange));
        }
        AI.SetActive(true);
        AI.transform.position = position;
        AI.GetComponent<NavMeshAgent>().speed = startspeed;
        AI.GetComponent<AIMove>().life = startlife;
        AI.GetComponent<animations>().Player = Player;
        startspeed *= incr_speed;
        startlife *= incr_life;
    }
}
