using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.VersionControl;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Texture2D map;

    public ListPrefab[] P;

    public void GenerateLab()
    {
        for (int j = 0; j < map.height; j++)
        {
            for (int i = 0; i < map.width; i++)
            {
                Color c = map.GetPixel(i, j);
                if (i % 2 == 0 && j % 2 == 0)
                {
                    if (c == Color.black)
                        create("Pillar", i, j);
                }
                else if (i % 2 == 1 && j % 2 == 0)
                {
                    if (c == Color.black)
                        create("WallZ", i, j);
                }
                else if (i % 2 == 0 && j % 2 == 1)
                {
                    if (c == Color.black)
                        create("WallX", i, j);
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    create("GroundTile", i, j);
                }
            }
        }
    }

    public void create(string name, int x,int y){
        Vector3 v;
        foreach (ListPrefab G in P)
            {
                if (G.name == name)
                {
                    switch (G.name)
                    {
                        case ("Pillar"):
                            v = new Vector3(2 * x, (float) 2.5, 2 * y);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("WallZ"):
                            v = new Vector3(2 * x, (float) 2.5, 2 * y);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("WallX"):
                            v = new Vector3(2 * x, (float) 2.5, 2 * y);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("GroundTile"):
                            v = new Vector3(2*x,0,2*y);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                    }
                }
            }
    }
    
    void Start()
    {
        GenerateLab();
    }
}
