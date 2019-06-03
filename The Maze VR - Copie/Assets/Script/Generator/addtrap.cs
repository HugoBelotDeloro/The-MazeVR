using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class addtrap : MonoBehaviour
{
    public ListPrefab[] P;

    private bool created;

    public GameObject parent;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        created = false;
    }

    public void send(string s)
    {
        
    }
    
    public void Receive(string s)
    {
        Vector3 v;
        string[] command = s.Split(':');
        switch (command[0])
        {
            case ("cd"):
                parent.GetComponentInChildren<Generator>().code = command[1];
                parent.GetComponentInChildren<Generator>().enabled = true;
                created = true;
                break;
            case ("pi"):
                if (created)
                {
                    foreach (ListPrefab G in P)
                    {
                        if (G.name == command[1])
                        {
                            v = new Vector3(2 * Convert.ToInt32(command[2])+transform.position.x, transform.position.y, 2 * Convert.ToInt32(command[3])+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                        }
                    }
                }
                break;
        }
        
    }
    //Send("pi:3")

    // Update is called once per frame
    void Update()
    {
        if (created)
        {
            player = GameObject.Find("Player(Clone)");
            send("pos:" + player.transform.position.x + ":" + player.transform.position.z);
        }
    }
}
