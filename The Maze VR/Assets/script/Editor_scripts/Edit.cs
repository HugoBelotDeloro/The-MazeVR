using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Edit : MonoBehaviour
{
    public GameObject parent;

    public int[,] map;

    private GameObject[,] mapgo;

    public ListPrefab[] P;

    public int currentobject = 2;

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
    
    // Start is called before the first frame update
    void Start()
    {
        Begin begin = parent.GetComponent<Begin>();
        map = new int[begin.H*2+1,begin.L*2+1];
        mapgo = new GameObject[begin.H*2+1,begin.L*2+1];
        Debug.Log(map.GetLength(0));
        Debug.Log(map.GetLength(1));
        finished = false;
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
                }
                else if (i % 2 == 0 && j % 2 == 1)
                {
                    if (c == 2)
                        create("Wall", i, j,false);
                    else if (c == 4)
                        create("Door", i, j,false);
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

    public void create(string name, int x,int y, bool rotate)
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
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Wall"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (rotate)
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
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Door"):
                            v = new Vector3(2 * x+transform.position.x, (float) 2.5+transform.position.y, 2 * y+transform.position.z);
                            if (rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Light"):
                            v=new Vector3(2*x+transform.position.x,(float)4.5 +transform.position.y,2*y+transform.position.z);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
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
        //wall et door
        if (currentobject == 2 || currentobject == 4)
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
            if (mapgo[posX, posY] == null)
            {
                if (rotation)
                    currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.Euler(new Vector3(0, 90, 0)),
                        transform);
                else
                    currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
            }
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
            if (mapgo[posX, posY] == null)
                currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
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
            if (mapgo[posX, posY] == null)
                currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
            //place
            done = false;
            placing = true;
        }
        else if (currentobject==P.Length)
        {
            if (mapgo[posX, posY] != null)
            {
                Debug.Log(mapgo[posX,posY].name);
                prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                {
                    prefabcolorlist.Add(r.material.color);
                    r.material.color=Color.red;
                }
            }
            done = false;
            placing = true;
        }
    }

    void Update()
    {
        Vector3 v;
        if (placing)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                finished = true;
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (currentobject == P.Length)
                {
                    if (mapgo[posX, posY] != null)
                    {
                        for (int i = 0; i < prefabcolorlist.Count; i++)
                        {
                            mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                        }
                        mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                    }
                    currentobject = 1;
                }
                else
                {
                    currentobject += 1;
                }
                done = true;
                Destroy(currentgameobjectprefab);
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if (currentobject==P.Length)
                {
                    if (mapgo[posX, posY] != null)
                    {
                        for (int i = 0; i < prefabcolorlist.Count; i++)
                        {
                            mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i];
                        }
                        mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
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
            }
            if (currentobject == P.Length)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("1");
                    if (posX > 0 && posY > 0)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posX -= 1;
                        posY -= 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 0)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posY -= 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 1 && posY > 0)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posX += 1;
                        posY -= 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 0)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posX -= 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    Destroy(mapgo[posX,posY]);
                    mapgo[posX, posY] = null;
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 1)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posX += 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 0 && posY < map.GetLength(0)-1)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posX -= 1;
                        posY += 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-1)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posY += 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 1 && posY < map.GetLength(0)-1)
                    {
                        if (mapgo[posX, posY] != null)
                        {
                            for (int i = 1; i < prefabcolorlist.Count-1; i++)
                            {
                                mapgo[posX, posY].GetComponentsInChildren<Renderer>()[i].material.color = prefabcolorlist[i-1];
                            }
                            mapgo[posX, posY].GetComponent<Renderer>().material.color = prefabcolor;
                        }
                        posX += 1;
                        posY += 1;
                        if (mapgo[posX, posY] != null)
                        {
                            prefabcolor = mapgo[posX, posY].GetComponent<Renderer>().material.color;
                            mapgo[posX, posY].GetComponent<Renderer>().material.color=Color.red;
                            prefabcolorlist.Clear();
                            foreach (Renderer r in mapgo[posX,posY].GetComponentsInChildren<Renderer>())
                            {
                                prefabcolorlist.Add(r.material.color);
                                r.material.color=Color.red;
                            }
                        }
                    }
                }
            }
            else if (currentobject == 2 || currentobject == 4)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("1");
                    if (posX > 0 && posY > 0)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 1;
                        posY -= 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 1 && posY > 0)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 1;
                        posY -= 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    if (currentgameobjectprefab != null)
                    {
                        map[posX, posY] = currentobject;
                        mapgo[posX, posY] = currentgameobjectprefab;
                        currentgameobjectprefab = null;
                    }
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 0 && posY < map.GetLength(0)-1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 1;
                        posY += 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 1 && posY < map.GetLength(0)-1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 1;
                        posY += 1;
                        rotation = !rotation;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                        {
                            if (rotation)
                                currentgameobjectprefab = Instantiate(currentgameobject, v,Quaternion.Euler(new Vector3(0, 90, 0)), transform);
                            else
                                currentgameobjectprefab =Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        }
                        else
                            currentgameobjectprefab = null;
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
                        posX -= 2;
                        posY -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posY -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 2 && posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        posY -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    if (currentgameobjectprefab != null)
                    {
                        map[posX, posY] = currentobject;
                        mapgo[posX, posY] = currentgameobjectprefab;
                        currentgameobjectprefab = null;
                    }
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 1 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 2;
                        posY += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posY += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 2 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        posY += 2;
                        if (currentobject==3)
                            v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
                        else
                            v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
                        if (mapgo[posX, posY] == null)
                            currentgameobjectprefab = Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab=null;
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
                        posX -= 2;
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("2");
                    if (posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Debug.Log("3");
                    if (posX < map.GetLength(1) - 2 && posY > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        posY -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Debug.Log("4");
                    if (posX > 1)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Debug.Log("5");
                    if (currentgameobjectprefab != null)
                    {
                        map[posX, posY] = currentobject;
                        mapgo[posX, posY] = currentgameobjectprefab;
                        currentgameobjectprefab = null;
                    }
                    done = true;
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Debug.Log("6");
                    if (posX < map.GetLength(1) - 2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Debug.Log("7");
                    if (posX > 1 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX -= 2;
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Debug.Log("8");
                    if (posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.Log("9");
                    if (posX < map.GetLength(1) - 2 && posY < map.GetLength(0)-2)
                    {
                        Destroy(currentgameobjectprefab);
                        posX += 2;
                        posY += 2;
                        v = new Vector3(2 * posX+transform.position.x, (float) 2.5+transform.position.y, 2 * posY+transform.position.z);
                        if (mapgo[posX,posY]==null)
                            currentgameobjectprefab=Instantiate(currentgameobject, v, Quaternion.identity, transform);
                        else
                            currentgameobjectprefab = null;
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
        }
    }

    void finishing()
    {
        string code = "";
        int x = map.GetLength(1);
        int y = map.GetLength(0);
    }
}