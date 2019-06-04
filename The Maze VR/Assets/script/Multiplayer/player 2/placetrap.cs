using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class placetrap : MonoBehaviour
{
    public GameObject parent;

    public Button b;

    public int[,] map;

    public ListPrefab[] structs;

    public ListPrefab[] pieges;

    private List<(ListPrefab,int,int)> piegesplaces;

    public int currentobject;

    public int posX = 0;

    public int posY = 0;

    private bool placing = false;

    private GameObject currentgameobject;

    private GameObject currentgameobjectprefab;

    private bool done;

    private bool rotation;

    private bool finished;

    public GameObject cursor;

    private GameObject cursorprefab;

    public Text nameobject;

    public Canvas can;

    public string Incode;

    private float energy;

    private int trapname;

    private int compteur;

    private Parser p;

    public Text energydisplay;

    public Text trapcost;

    private GameObject player;

    public GameObject IA;

    private GameObject IApos;

    public GameObject IAghost;

    private GameObject IAghostpos;

    public TextMeshProUGUI won;

    public TextMeshProUGUI lost;
    
    // Start is called before the first frame update
    void Start()
    {
        begingame begin = b.GetComponent<begingame>();
        p = gameObject.AddComponent<Parser>();
        trapname = 0;
        currentobject = 0;
        piegesplaces = new List<(ListPrefab, int, int)>();
        Incode = begin.code;
        send("cd:"+Incode);
        map = p.codeToMap(Incode);
        finished = false;
        posX = 0;
        energy = 20;
        compteur = 0;
        posY = map.GetLength(0) - 1;
        GenerateLab();
        Place();
    }

    public void GenerateLab()
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                int c = map[i,j];
                if (i % 2 == 0 && j % 2 == 0)
                {
                    if (c == 1)
                        create("Pillar", i, j,false);
                }
                else if (i % 2 == 1 && j % 2 == 0)
                {
                    if (c == 2)
                        create("Wall", i, j,true);
                    else if (c == 4)
                        create("Door", i, j,true);
                    else if (c == 6)
                        create("FalseWall", i, j,true);
                    else if (c == 7)
                        create("IlluWall", i, j,true);
                    else if (c == 9)
                        create("DoorLock", i, j,true);
                }
                else if (i % 2 == 0 && j % 2 == 1)
                {
                    if (c == 2)
                        create("Wall", i, j,false);
                    else if (c == 4)
                        create("Door", i, j,false);
                    else if (c == 6)
                        create("FalseWall", i, j,false);
                    else if (c == 7)
                        create("IlluWall", i, j,false);
                    else if (c == 9)
                        create("DoorLock", i, j,false);
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    if (c== 5)
                        create("Light",i,j,false);
                    if (c== 3)
                        create("Player",i,j,false);
                    else if (c==8)
                        create("Goal",i,j,false);
                    else if (c == 10)
                        create("Key",i,j,false);
                    create("GroundTile", i, j,false);
                }
            }
        }
    }

    public void create(string name, int y,int x, bool rotate)
    {
        Vector3 v;
        foreach (ListPrefab G in structs)
            {
                if (G.name == name)
                {
                    switch (G.name)
                    {
                        case ("Pillar"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Wall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("GroundTile"):
                            v = new Vector3(2*x+transform.position.x,0+transform.position.y,2*y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Player"):
                            v=new Vector3(2*x+transform.position.x,(float)1.5 +transform.position.y,2*y+transform.position.z);
                            player = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            map[y, x] = -1;
                            break;
                        case ("Door"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Light"):
                            v=new Vector3(2*x+transform.position.x,(float)4.5 +transform.position.y,2*y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("FalseWall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("IlluWall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Goal"):
                            v=new Vector3(2*x+transform.position.x,(float)4.5 +transform.position.y,2*y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.Euler(0,45,0), transform);
                            break;
                        case ("DoorLock"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Key"):
                            v=new Vector3(2*x+transform.position.x,(float)4.5 +transform.position.y,2*y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.Euler(0,45,0), transform);
                            break;
                    }
                }
            }
    }

    void Place()
    {
        //variables
        if (currentobject!=pieges.Length)
            currentgameobject = pieges[currentobject].prefab;
        Vector3 v;
        Vector3 cv;
        //wall et door
        if (currentobject == 3)
        {
            //rectifie position
            if (posX % 2 == 0 && posY % 2 == 0)
            {
                if (posX == 0)
                    posX += 1;
                else
                    posX -= 1;
            }
            if (posX % 2 == 1 && posY % 2 == 1)
            {
                posY += 1;
            }
            //creer objet
            rotation = posX % 2 != 0;
            v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
            cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
            if (map[posY, posX] == -1)
            {
                if (rotation)
                    currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.Euler(new Vector3(0, 90, 0)),transform);
                else
                    currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
            }
            else
                currentgameobjectprefab = null;
            cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
            //place
            done = false;
            placing = true;
        }
        //player et light
        else if (currentobject == 0 || currentobject == 1 || currentobject == 2)
        {
            //rectifie position
            if (posX % 2 == 0)
            {
                if (posX == 0)
                    posX += 1;
                else
                    posX -= 1;
            }
            if (posY % 2 == 0)
            {
                if (posY == 0)
                    posY += 1;
                else
                    posY -= 1;
            }
            //creer objet
            v=new Vector3(2*posX+transform.position.x,(float)0.5 +transform.position.y,2*posY+transform.position.z);
            cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
            if (map[posY, posX] == -1)
                currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
            else
                currentgameobjectprefab = null;
            cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
            //place
            done = false;
            placing = true;
        }
        //pillar
        else if (false) //TODO
        {
            //rectifie position
            if (posX % 2 == 1)
            {
                posX += 1;
            }
            if (posY % 2 == 1)
            {
                posY += 1;
            }
            //creer objet
            v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
            cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
            if (map[posY, posX] == -1)
                currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
            else
                currentgameobjectprefab = null;
            cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
            //place
            done = false;
            placing = true;
        }
        else if (currentobject == pieges.Length)
        {
            done = false;
            placing = true;
        }
    }
    
    public void send(string s)
    {
        GameObject.Find("Client").GetComponent<client>().GameCmd(s);
    }

    public void Receive(string s)
    {
        string[] command = s.Split(':');
        switch (command[1])
        {
            case ("pos"):
                Vector3 playerpos = new Vector3(Convert.ToSingle(command[2]),(float)1.5,Convert.ToSingle(command[3]));
                player.transform.position = playerpos;
                break;
            case ("end"):
                switch (command[2])
                {
                    case ("1"):
                        lost.GetComponent<TextMeshProUGUI>().enabled = true;
                        GameObject.Find("Client").GetComponent<client>().Win();
                        break;
                    case ("2"):
                        won.GetComponent<TextMeshProUGUI>().enabled = true;
                        GameObject.Find("Client").GetComponent<client>().GameOver();
                        break;
                }
                finished = true;
                break;
            case ("act"):
                for (int i = 0; i < piegesplaces.Count; i++)
                {
                    if (piegesplaces[i].Item1.name == command[2])
                    {
                        map[piegesplaces[i].Item3, piegesplaces[i].Item2] = -1;
                        Destroy(piegesplaces[i].Item1.prefab);
                        piegesplaces.Remove(piegesplaces[i]);
                    }
                }
                break;
            case ("IA"):
                if (command[2] == "1")
                {
                    if (IApos != null)
                    {
                        Vector3 iapos = new Vector3(Convert.ToSingle(command[3]),(float)1.5f,Convert.ToSingle(command[4]));
                        IApos.transform.position = iapos;
                    }
                    else
                    {
                        Vector3 iapos = new Vector3(Convert.ToSingle(command[3]),(float)1.5f,Convert.ToSingle(command[4]));
                        IApos = Instantiate(IA, iapos, Quaternion.identity, transform);
                    }
                }
                else
                {
                    Destroy(IApos);
                }

                break;
        }
    }

    private void FixedUpdate()
    {
        if (energy < 20)
        {
            if (compteur == 25)
            {
                energy+=0.25f;
                compteur = 0;
            }
            else
            {
                compteur++;
            }
        }
    }

    void Update()
    {
        if (currentobject == pieges.Length)
            nameobject.text = "Current trap : No lights";
        else
            nameobject.text = "Current trap : "+pieges[currentobject].name;
        energydisplay.text = "Energy : " + energy+"/20";
        Vector3 v;
        Vector3 cv;
        
        if (placing)
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (currentobject == pieges.Length)
                {
                    currentobject = 0;
                }
                else
                {
                    currentobject += 1;
                }
                done = true;
                Destroy(currentgameobjectprefab);
                Destroy(cursorprefab);
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if (currentobject == 0)
                {
                    currentobject = pieges.Length;
                }
                else
                {
                    currentobject -= 1;
                }
                done = true;
                Destroy(currentgameobjectprefab);
                Destroy(cursorprefab);
            }
            else if (currentobject == pieges.Length)
            {
                trapcost.text = "Cost : 10";
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    if (energy >= 10)
                    {
                        energy -= 10;
                        send("pi:" + pieges.Length);
                    }
                }
            }
            else if (currentobject == 3)
            {
                trapcost.text = "Cost : 5";
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("1");
                    if (posX > 0 && posY > 0)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 1;
                        posY -= 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 1 && posY > 0)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 1;
                        posY -= 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    if (currentgameobjectprefab != null)
                    {
                        if (energy >= 5)
                        {
                            energy -= 5;
                            if (rotation)
                                send("pi:" + currentobject + ":" + posY + ":" + posX + ":" + trapname+":r");
                            else
                                send("pi:" + currentobject + ":" + posY + ":" + posX + ":" + trapname+":n");
                            piegesplaces.Add((new ListPrefab(currentgameobjectprefab, trapname.ToString()),posX,posY));
                            map[posY, posX] = currentobject;
                            currentgameobjectprefab = null;
                            trapname++;
                        }
                        else
                        {
                            Destroy(currentgameobjectprefab);
                        }
                    }
                    Destroy(cursorprefab);
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 0 && posY < map.GetLength(0)-1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 1;
                        posY += 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 1 && posY < map.GetLength(0)-1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 1;
                        posY += 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
            }
            else if (currentobject == 0 || currentobject == 1 || currentobject == 2)
            {
                trapcost.text = "Cost : 7.5";
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("1");
                    if (posX > 1 && posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        posY -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posY -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 2 && posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        posY -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    if (currentgameobjectprefab != null)
                    {
                        if (energy >= 7.5f)
                        {
                            energy -= 7.5f;
                            send("pi:" + currentobject + ":" + posY + ":" + posX + ":" + trapname);
                            piegesplaces.Add((new ListPrefab(currentgameobjectprefab, trapname.ToString()),posX,posY));
                            map[posY, posX] = currentobject;
                            currentgameobjectprefab = null;
                            trapname++;
                        }
                        else
                        {
                            Destroy(currentgameobjectprefab);
                        }
                    }
                    Destroy(cursorprefab);
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 1 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        posY += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posY += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 2 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        posY += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
            }
            else if (false)//TODO
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("1");
                    if (posX > 1 && posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 2 && posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))//TODO
                {
                    send("pi:"+currentobject+":"+posY+":"+posX+":"+trapname);
                    piegesplaces.Add((new ListPrefab(currentgameobjectprefab,trapname.ToString()),posX,posY));
                    map[posY, posX] = currentobject;
                    currentgameobjectprefab = null;
                    Destroy(cursorprefab);
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 1 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX -= 2;
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 2 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        Destroy(cursorprefab);
                        posX += 2;
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (map[posY, posX] == -1)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
            }
        }
        if (done)
        {
            placing = false;
            Place();
        }

        if (finished)
        {
            finishing();
            finished = false;
        }
    }

    public void finishing()
    {
        can.GetComponent<Canvas>().enabled = true;
        can.GetComponent<GoToMainMenu>().enabled = true;
    }
}
