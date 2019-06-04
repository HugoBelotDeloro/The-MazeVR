using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class addtrap : MonoBehaviour
{
    public ListPrefab[] P;

    private bool created;

    private bool playing;

    public GameObject parent;

    private GameObject player;

    [SerializeField] private GameObject IAspawner;

    private int health;

    private List<(ListPrefab,int)> listpieges;

    private GameObject g;

    public List<int> remove;

    [SerializeField] private Item compass;
    
    [SerializeField] private Item clothes;
    
    [SerializeField] private Item light;

    private bool equiped;

    public bool win;
    
    public Canvas can;
    
    public List<GameObject> lamps;

    public GameObject Ia;

    public bool compteur;
    
    private int c;

    public TextMeshProUGUI won;

    public TextMeshProUGUI lost;
    
    // Start is called before the first frame update
    void Start()
    {
        remove = new List<int>();
        listpieges = new List<(ListPrefab, int)>();
        created = false;
        equiped = false;
        win = false;
        playing = true;
        c = 0;
        compteur = false;
    }

    public void send(string s)
    {
        GameObject.Find("Client").GetComponent<client>().GameCmd(s);
    }
    
    public void Receive(string s)
    {
        Vector3 v;
        string[] command = s.Split(':');
        switch (command[1])
        {
            case ("cd"):
                parent.GetComponent<Generator>().code = command[2];
                parent.GetComponent<Generator>().enabled = true;
                created = true;
                break;
            case ("pi"):
                if (created)
                {
                    for (int i = 0; i < P.Length; i++)
                    {
                        if (i == Convert.ToInt32(command[2]))
                        {
                            if (i == 3)
                            {
                                v = new Vector3(2 * Convert.ToInt32(command[4]) + transform.position.x,2.5f + transform.position.y,2 * Convert.ToInt32(command[3]) + transform.position.z);
                                if (command[6]=="n")
                                    g = Instantiate(P[i].prefab, v, Quaternion.identity, transform);
                                else
                                    g = Instantiate(P[i].prefab, v, Quaternion.Euler(0, 90, 0), transform);
                                listpieges.Add((new ListPrefab(g, command[5]), 750));
                            }
                            else
                            {
                                v = new Vector3(2 * Convert.ToInt32(command[4]) + transform.position.x,0.5f + transform.position.y,2 * Convert.ToInt32(command[3]) + transform.position.z);
                                g = Instantiate(P[i].prefab, v, Quaternion.Euler(-90, 0, 0), transform);
                                listpieges.Add((new ListPrefab(g, command[5]), 750));
                            }
                        }
                    }
                    if (Convert.ToInt32(command[2]) == P.Length)
                    {
                        if (!compteur)
                        {
                            foreach (GameObject lamp in lamps)
                            {
                                lamp.GetComponent<Switch>().switching();
                            }
                            compteur = true;
                            c = 500;
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
                player.GetComponentInChildren<Inventory>().AddItem(clothes);
                player.GetComponentInChildren<Inventory>().AddItem(compass);
                player.GetComponentInChildren<Inventory>().AddItem(light);
                GameObject iasp = Instantiate(IAspawner, Vector3.zero,Quaternion.identity,transform);
                iasp.GetComponent<AIspawn>().Player = GameObject.Find("Player(Clone)");
                lamps = parent.GetComponent<Generator>().lamps;
                equiped = true;
            }
            health = player.GetComponent<PlayerHealth>().Health;
            send("pos:" + player.transform.position.x + ":" + player.transform.position.z);
            if (GameObject.Find("IA(Clone)" )!= null) //je crois que ya tj des majuscules a clone / thx, j'avais oublié
            {
                Ia = GameObject.Find("IA(Clone");
                send("IA:1:"+Ia.transform.position.x+":"+Ia.transform.position.z);
            }
            else
            {
                send("IA:0");
            }
            if (health<=0)
            {
                lost.GetComponent<TextMeshProUGUI>().enabled = true;
                send("end:2");
                finishing();
            }
            if (win)
            {
                won.GetComponent<TextMeshProUGUI>().enabled = true;
                send("end:1");
                finishing();
            }

            if (compteur)
            {
                c -= 1;
                if (c == 0)
                {
                    foreach (GameObject lamp in lamps)
                    {
                        lamp.GetComponent<Switch>().switching();
                    }
                    compteur = false;
                }
            }
            for (int i = 0; i < listpieges.Count; i++)
            {
                (ListPrefab, int) piaij = listpieges[i];
                piaij.Item2 -= 1;
                listpieges[i] = piaij;
                Debug.Log(piaij.Item2);
                if (piaij.Item2 <= 0)
                {
                    listpieges[i].Item1.prefab.GetComponent<Trap>().act = true;
                }
                if (listpieges[i].Item1.prefab.GetComponent<Trap>().act)
                {
                    Destroy(listpieges[i].Item1.prefab);
                    send("act:"+listpieges[i].Item1.name);
                    listpieges.Remove(listpieges[i]);
                }
            }
        }
    }
    
    public void finishing()
    {
        can.GetComponent<Canvas>().enabled = true;
        can.GetComponent<GoToMainMenu>().enabled = true;
        playing = false;
    }
}
