using System.Collections;
using System.Collections.Generic;
// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class MoveTo : MonoBehaviour

{
    private NavMeshAgent agent;
    public GameObject target;
    private float timer = 0;
    public float GoStraightDistance; //if the monster is close enough, he stop making mistakes and find the player
    public float Error; //error rate between 0 and 1, error decrease with distance
    public float ResetDestTimer; //how many seconds does the monster keep the wrong destination before picking another
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        timer += ResetDestTimer;
    }
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < GoStraightDistance)
        {
            agent.destination = target.transform.position;
        }
        else if (timer > 9)
        {
            agent.destination = new Vector3(target.transform.position.x + UnityEngine.Random.Range(-Error * Vector3.Distance(target.transform.position, transform.position), Error * Vector3.Distance(target.transform.position, transform.position)), target.transform.position.y, target.transform.position.z + UnityEngine.Random.Range(-Error * Vector3.Distance(target.transform.position, transform.position), Error * Vector3.Distance(target.transform.position, transform.position)));
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
