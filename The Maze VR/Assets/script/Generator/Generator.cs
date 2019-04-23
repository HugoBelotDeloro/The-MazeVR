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
                    if (c == new Color(0,0,0,1))
                        create("Pillar", i, j,false);
                }
                else if (i % 2 == 1 && j % 2 == 0)
                {
                    if (c ==new Color(0,0,0,1))
                        create("Wall", i, j,true);
                    else if (c ==new Color(1,1,0,1))
                        create("Door", i, j,true);
                }
                else if (i % 2 == 0 && j % 2 == 1)
                {
                    if (c ==new Color(0,0,0,1))
                        create("Wall", i, j,false);
                    else if (c ==new Color(1,1,0,1))
                        create("Door", i, j,false);
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    if (c==new Color(1, 0.498039216f, 0,1))
                        create("Light",i,j,false);
                    if (c==new Color(1,0,0,1))
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
    
    void Start()
    {
        GenerateLab();
    }
}
