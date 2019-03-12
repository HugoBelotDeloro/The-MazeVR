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
                        create("Pillar", i, j,false);
                }
                else if (i % 2 == 1 && j % 2 == 0)
                {
                    if (c == Color.black)
                        create("Wall", i, j,true);
                }
                else if (i % 2 == 0 && j % 2 == 1)
                {
                    if (c == Color.black)
                        create("Wall", i, j,false);
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    if (c==Color.red)
                        create("Player",i,j,false);
                    create("GroundTile", i, j,false);
                }
            }
        }
    }

    public void create(string name, int x,int y, bool rotate){
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
                        case ("Wall"):
                            v = new Vector3(2 * x, (float) 2.5, 2 * y);
                            if (rotate)
                                Instantiate(G.prefab, v,Quaternion.Euler(new Vector3(0,90,0)), transform);
                            else
                                Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("GroundTile"):
                            v = new Vector3(2*x,0,2*y);
                            Instantiate(G.prefab, v, Quaternion.identity, transform);
                            break;
                        case ("Player"):
                            v=new Vector3(2*x,(float)1.5 ,2*y);
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
