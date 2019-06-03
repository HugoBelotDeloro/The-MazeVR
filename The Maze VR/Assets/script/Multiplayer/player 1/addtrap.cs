using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class addtrap : MonoBehaviour
{
    public ListPrefab[] P;

    public ListPrefab[] pieges;

    private bool created;

    private bool playing;

    public GameObject parent;

    private GameObject player;

    private int health;

    private List<(ListPrefab,int)> listpieges;

    private GameObject g;

    public List<int> remove;

    [SerializeField] private Item compass;
    
    [SerializeField] private Item clothes;
    
    [SerializeField] private Item light;

    [SerializeField] private Item stone;

    private bool equiped;

    public bool win;
    
    public Canvas can;
    
    public Text winmessage;
    
    public List<GameObject> lamps;

    public (bool, int) compteur;
    
    // Start is called before the first frame update
    void Start()
    {
        created = false;
        equiped = false;
        win = false;
    }

    public void send(string s)
    {
        GameObject.Find("client").GetComponent<client>().GameCmd(s);
        compteur = (false, 0);
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
                            g.GetComponent<Trap>().ID = Convert.ToInt32(command[4]);
                            listpieges.Add((new ListPrefab(g,command[4]),750));
                        }
                    }
                    if (Convert.ToInt32(command[1]) == pieges.Length)
                    {
                        if (!compteur.Item1)
                        {
                            foreach (GameObject lamp in lamps)
                            {
                                lamp.GetComponent<Switch>().switching();
                            }
                            compteur = (true, 500);
                        }
                    }
                }
                break;
        }
        
    }
    void FixedUpdate()
    {
        if (created && playing)
        {
            if (!equiped)
            {
                player = GameObject.Find("Player(Clone)");
                player.GetComponent<Inventory>().AddItem(clothes);
                player.GetComponent<Inventory>().AddItem(compass);
                player.GetComponent<Inventory>().AddItem(light);
                lamps = parent.GetComponentInChildren<Generator>().lamps;
                equiped = true;
            }
            health = player.GetComponent<PlayerHealth>().Health;
            send("pos:" + player.transform.position.x + ":" + player.transform.position.z);
            if (health<=0)
            {
                winmessage.text = "DEFEAT";
                send("end:2");
                finishing();
            }
            if (win)
            {
                winmessage.text = "VICTORY";
                send("end:1");
                finishing();
            }

            if (compteur.Item1)
            {
                int c = compteur.Item2;
                c -= 1;
                if (c == 0)
                {
                    foreach (GameObject lamp in lamps)
                    {
                        lamp.GetComponent<Switch>().switching();
                    }
                    compteur.Item1 = false;
                }
            }
            while (remove.Count>0)
            {
                send("act:"+remove[0]);
                remove.Remove(0);
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
    
    public void finishing()
    {
        can.GetComponent<Canvas>().enabled = true;
        playing = false;
    }
}
