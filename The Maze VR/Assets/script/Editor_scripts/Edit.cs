using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Edit : MonoBehaviour
{
    public GameObject parent;

    public int[,] map;

    public ListPrefab[] P;

    private int currentobject = 3;

    private int posX = 0;

    private int posY = 0;

    private ConsoleKey c;

    private bool keydone;
    
    // Start is called before the first frame update
    void Start()
    {
        Begin begin = parent.GetComponent<Begin>();
        map=new int[begin.H*2+1,begin.L*2+1];
        GenerateLab();
        Editing();
    }

    public void GenerateLab()
    {
        for (int j = 0; j < map.GetLength(0); j++)
        {
            for (int i = 0; i < map.GetLength(1); i++)
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
        GameObject g = P[currentobject].prefab;
        ConsoleKey c;
        bool done = false;
        Vector3 v;
        //wall et door
        if (currentobject == 2 || currentobject == 4)
        {
            //rectifie position
            if (posX+posY % 2 != 1)
            {
                if (posX == map.GetLength(1))
                    posX -= 1;
                else
                    posX += 1;
            }
            //place
            while (!done)
            {
                c = Console.ReadKey().Key;
                
            }

        }
        //player et light
        else if (currentobject == 3 || currentobject == 5)
        {
            //rectifie position
            if (posX % 2 == 0)
            {
                if (posX == map.GetLength(1))
                    posX -= 1;
                else
                    posX += 1;
            }
            if (posY % 2 == 1)
            {
                if (posY == map.GetLength(0))
                    posY -= 1;
                else
                    posY += 0;
            }
            //creer objet
            if (currentobject==3)
                v=new Vector3(2*posX+transform.position.x,(float)1.5 +transform.position.y,2*posY+transform.position.z);
            else
                v=new Vector3(2*posX+transform.position.x,(float)4.5 +transform.position.y,2*posY+transform.position.z);
            Instantiate(g, v, Quaternion.identity, transform);
            //place
            while (!done)
            {
                keydone = false;
                StartCoroutine(getkey2());
                while (!keydone){}
                StopCoroutine(getkey2());
                switch (this.c)
                {
                    case(ConsoleKey.Add)://change objet +
                        if (currentobject == P.Length - 1)
                        {
                            currentobject = 1;
                        }
                        else
                        {
                            currentobject += 1;
                        }
                        done = true;
                        Destroy(g.transform);
                        break;
                    case (ConsoleKey.Subtract)://change objet -
                        if (currentobject == 1)
                        {
                            currentobject = P.Length-1;
                        }
                        else
                        {
                            currentobject -= 1;
                        }
                        done = true;
                        Destroy(g.transform);
                        break;
                    case (ConsoleKey.NumPad1):
                        if (posX != 1 && posY != map.GetLength(0) - 1)
                        {
                            posX -= 2;
                            posY += 2;
                            g.transform.position += transform.right * -2 + transform.forward * 2;
                        }
                        break;
                    case (ConsoleKey.NumPad2):
                        if (posY != map.GetLength(0) - 1)
                        {
                            posY += 2;
                            g.transform.position += transform.forward * 2;
                        }
                        break;
                    case (ConsoleKey.NumPad3):
                        if (posX != map.GetLength(1)-1 && posY != map.GetLength(0) - 1)
                        {
                            posX += 2;
                            posY += 2;
                            g.transform.position += transform.right * 2 + transform.forward * 2;
                        }
                        break;
                    case (ConsoleKey.NumPad4):
                        if (posX != 1)
                        {
                            posX -= 2;
                            g.transform.position += transform.right * -2;
                        }
                        break;
                    case (ConsoleKey.NumPad6):
                        if (posX != map.GetLength(1)-1)
                        {
                            posX += 2;
                            g.transform.position += transform.right * 2;
                        }
                        break;
                    case (ConsoleKey.NumPad7):
                        if (posX != 1 && posY != 1)
                        {
                            posX -= 2;
                            posY -= 2;
                            g.transform.position += transform.right * -2 + transform.forward * -2;
                        }
                        break;
                    case (ConsoleKey.NumPad8):
                        if (posY != 1)
                        {
                            posY -= 2;
                            g.transform.position += transform.forward * -2;
                        }
                        break;
                    case (ConsoleKey.NumPad9):
                        if (posX != map.GetLength(1)-1 && posY != 1)
                        {
                            posX += 2;
                            posY -= 2;
                            g.transform.position += transform.right * 2 + transform.forward * -2;
                        }
                        break;
                    case (ConsoleKey.NumPad5):
                    {
                        done = true;
                        break;
                    }
                }
            }
            
        }
        //pillar
        else if (currentobject == 1)
        {
            //rectifie position
            if (posX % 2 == 1)
            {
                if (posX == map.GetLength(1)-1)
                    posX -= 1;
                else
                    posX += 1;
            }
            if (posY % 2 == 1)
            {
                if (posY == map.GetLength(0)-1)
                    posY -= 1;
                else
                    posY += 1;
            }
            //place
            while (!done)
            {
                c = Console.ReadKey().Key;
                
            }
            
        }
    }

    void Editing()
    {
        Place();
    }

    IEnumerator getkey2()
    {
        bool done = false;
        while (!done)
        {
            yield return new WaitUntil(()=>Input.anyKeyDown);
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                c = ConsoleKey.NumPad1;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                c = ConsoleKey.NumPad2;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                c = ConsoleKey.NumPad3;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                c = ConsoleKey.NumPad4;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                c = ConsoleKey.NumPad5;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                c = ConsoleKey.NumPad6;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                c = ConsoleKey.NumPad7;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                c = ConsoleKey.NumPad8;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                c = ConsoleKey.NumPad9;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                c = ConsoleKey.Add;
                done = true;
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                c = ConsoleKey.Subtract;
                done = true;
            }
        }
        keydone = true;
    }
}
