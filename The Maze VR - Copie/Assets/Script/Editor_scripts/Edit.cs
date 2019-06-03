using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Edit : MonoBehaviour
{
    public GameObject parent;

    public int[,] map;

    private GameObject[,] mapgo;

    public ListPrefab[] P;

    public int currentobject = 1;

    public int posX = 0;

    public int posY = 0;

    private ConsoleKey c;

    private bool placing = false;

    private GameObject currentgameobject;

    private GameObject currentgameobjectprefab;

    private bool done;

    private bool rotation;

    private bool finished;

    public Color prefabcolor;

    public List<Color> prefabcolorlist;

    public GameObject cursor;

    private GameObject cursorprefab;

    public Text nameobject;
    
    private Parser p = new Parser();

    public InputField code;

    public Canvas can;

    public bool creating;

    public string Incode;
    
    // Start is called before the first frame update
    void Start()
    {
        Begin begin = parent.GetComponent<Begin>();
        creating = begin.create;
        Incode = begin.code;
        if (creating)
        {
            map = new int[begin.H * 2 + 1, begin.L * 2 + 1];
            mapgo = new GameObject[begin.H * 2 + 1, begin.L * 2 + 1];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = -1;
                }
            }

            Debug.Log(map.GetLength(0));
            Debug.Log(map.GetLength(1));
        }
        else
        {
            map = p.codeToMap(Incode);
            mapgo = new GameObject[map.GetLength(0),map.GetLength(1)];
        }
        finished = false;
        posX = 0;
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
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    if (c== 5)
                        create("Light",i,j,false);
                    if (c== 3)
                        create("Player",i,j,false);
                    create("GroundTile", i, j,false);
                }
            }
        }
    }

    public void create(string name, int y,int x, bool rotate)
    {
        Vector3 v;
        foreach (ListPrefab G in P)
            {
                if (G.name == name)
                {
                    switch (G.name)
                    {
                        case ("Pillar"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Wall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                mapgo[y,x] = Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("GroundTile"):
                            v = new Vector3(2*x+transform.position.x,0+transform.position.y,2*y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Player"):
                            v=new Vector3(2*x+transform.position.x,(float)1.5 +transform.position.y,2*y+transform.position.z);
                            mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Door"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                mapgo[y,x] = Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Light"):
                            v=new Vector3(2*x+transform.position.x,(float)4.5 +transform.position.y,2*y+transform.position.z);
                            mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("FalseWall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                mapgo[y,x] = Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("IlluWall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (!rotate)
                                mapgo[y,x] = Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                mapgo[y,x] = Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                    }
                }
            }
    }

    void Place()
    {
        //variables
        if (currentobject!=P.Length)
            currentgameobject = P[currentobject].prefab;
        Vector3 v;
        Vector3 cv;
        //wall et door
        if (currentobject == 2 || currentobject == 4 || currentobject == 6 || currentobject == 7)
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
            if (mapgo[posY, posX] == null)
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
        else if (currentobject == 3 || currentobject == 5)
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
            if (currentobject==3)
                v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
            else
                v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
            cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
            if (mapgo[posY, posX] == null)
                currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
            else
                currentgameobjectprefab = null;
            cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
            //place
            done = false;
            placing = true;
        }
        //pillar
        else if (currentobject == 1)
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
            if (mapgo[posY, posX] == null)
                currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
            else
                currentgameobjectprefab = null;
            cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
            //place
            done = false;
            placing = true;
        }
        else if (currentobject==P.Length)
        {
            if (mapgo[posY, posX] != null)
            {
                Debug.Log(mapgo[posY, posX].name);
                prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                prefabcolorlist.Clear();
                foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                {
                    prefabcolorlist.Add(r.material.color);
                    r.material.color=Color.red;
                }
            }
            done = false;
            placing = true;
            cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
            cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
        }
    }

    void Update()
    {
        if (currentobject!=P.Length)
            nameobject.text = P[currentobject].name;
        else
            nameobject.text = "Destroy";
        Vector3 v;
        Vector3 cv;
        if (placing)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Destroy(currentgameobjectprefab);
                Destroy(cursorprefab);
                finished = true;
                done = true;
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (currentobject == P.Length)
                {
                    if (mapgo[posY, posX] != null)
                    {
                        if (prefabcolorlist.Count <= 2)
                        {
                            for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                            {
                                mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                    prefabcolorlist[i - 1];
                            }
                        }
                        else
                        {
                            for (int i = 0; i < prefabcolorlist.Count; i++)
                            {
                                mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                            }
                        }
                        mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                    }
                    currentobject = 1;
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
                if (currentobject==P.Length)
                {
                    if (mapgo[posY, posX] != null)
                    {
                        if (prefabcolorlist.Count <= 2)
                        {
                            for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                            {
                                mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                    prefabcolorlist[i - 1];
                            }
                        }
                        else
                        {
                            for (int i = 0; i < prefabcolorlist.Count; i++)
                            {
                                mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                            }
                        }
                        mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                    }
                }
                if (currentobject == 1)
                {
                    currentobject = P.Length;
                }
                else
                {
                    currentobject -= 1;
                }
                done = true;
                Destroy(currentgameobjectprefab);
                Destroy(cursorprefab);
            }
            if (currentobject == P.Length)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("1");
                    if (posX > 0 && posY > 0)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posX -= 1;
                        posY -= 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 0)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posY -= 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 1 && posY > 0)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posX += 1;
                        posY -= 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 0)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posX -= 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    Destroy(mapgo[posY, posX]);
                    Destroy(cursorprefab);
                    mapgo[posY, posX] = null;
                    map[posY, posX] = -1;
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 1)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posX += 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 0 && posY < map.GetLength(0)-1)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posX -= 1;
                        posY += 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-1)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posY += 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 1 && posY < map.GetLength(0)-1)
                    {
                        if (mapgo[posY, posX] != null)
                        {
                            if (prefabcolorlist.Count <= 2)
                            {
                                for (int i = 1; i < prefabcolorlist.Count - 1; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color =
                                        prefabcolorlist[i - 1];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < prefabcolorlist.Count; i++)
                                {
                                    mapgo[posY, posX].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                                }
                            }
                            mapgo[posY, posX].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        Destroy(cursorprefab);
                        posX += 1;
                        posY += 1;
                        if (mapgo[posY, posX] != null)
                        {
                            prefabcolor = mapgo[posY, posX].GetComponent<Renderer>().material.color;
                            mapgo[posY, posX].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posY, posX].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
            }
            else if (currentobject == 2 || currentobject == 4 || currentobject == 6 || currentobject == 7)
            {
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        map[posY, posX] = currentobject;
                        mapgo[posY, posX] = currentgameobjectprefab;
                        currentgameobjectprefab = null;
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
            else if (currentobject == 3 || currentobject == 5)
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
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        cv = new Vector3((float) (2*posX+transform.position.x-0.5), 10, (float) (2*posY+transform.position.z-0.5));
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        map[posY, posX] = currentobject;
                        mapgo[posY, posX] = currentgameobjectprefab;
                        currentgameobjectprefab = null;
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
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
                        if (mapgo[posY, posX] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                        cursorprefab = Instantiate(cursor, cv, Quaternion.Euler(90,0,0), transform);
                    }
                }
            }
            else if (currentobject == 1)
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
                        if (mapgo[posY, posX]==null)
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
                        if (mapgo[posY, posX]==null)
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
                        if (mapgo[posY, posX]==null)
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
                        if (mapgo[posY, posX]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
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
                        map[posY, posX] = currentobject;
                        mapgo[posY, posX] = currentgameobjectprefab;
                        currentgameobjectprefab = null;
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
                        if (mapgo[posY, posX]==null)
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
                        if (mapgo[posY, posX]==null)
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
                        if (mapgo[posY, posX]==null)
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
                        if (mapgo[posY, posX]==null)
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
        code.text = p.mapToCode(map);
        Debug.Log(p.mapToCode(map));
    }
}