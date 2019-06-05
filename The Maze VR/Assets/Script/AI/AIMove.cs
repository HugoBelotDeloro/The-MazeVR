using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;
    public float life; //number of second the AI is allowed to live

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
        life -= Time.deltaTime;
        if (life <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("AI died");
        }
    }
}
