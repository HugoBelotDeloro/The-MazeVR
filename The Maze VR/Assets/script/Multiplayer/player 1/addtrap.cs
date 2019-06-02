using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class addtrap : MonoBehaviour
{
    public ListPrefab[] P;

    private bool created;

    private bool playing;

    public GameObject parent;

    private GameObject player;

    private int health;

    private List<(ListPrefab,int)> listpieges;

    private GameObject g;
    
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
                    for (int i = 0; i < P.Length; i++)
                    {
                        if (i == Convert.ToInt32(command[1]))
                        {
                            v = new Vector3(2 * Convert.ToInt32(command[2])+transform.position.x, 0.5f+transform.position.y, 2 * Convert.ToInt32(command[3])+transform.position.z);
                            g = Instantiate(P[i].prefab, v, Quaternion.identity, transform);
                            listpieges.Add((new ListPrefab(g,command[4]),150));
                        }
                    }
                }
                break;
        }
        
    }
    //Send("pi:3")

    // Update is called once per frame
    void FixedUpdate()
    {
        if (created && playing)
        {
            player = GameObject.Find("Player(Clone)");
            health = player.GetComponent<PlayerHealth>().Health;
            send("pos:" + player.transform.position.x + ":" + player.transform.position.z +":"+health);
            if (health<=0)
            {
                send("end:2");
            }
            if (false)
            {
                send("end:1");
            }

            for (int i = 0; i < listpieges.Count; i++)
            {
                int l = listpieges[i].Item2;
                l -= 1;
                if (l == 0)
                {
                    send("act:"+listpieges[i].Item1.name);
                }
            }
        }
    }
}
