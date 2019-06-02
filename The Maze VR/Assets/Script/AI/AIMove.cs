using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }
}
