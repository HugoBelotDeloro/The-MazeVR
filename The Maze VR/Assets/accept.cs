using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accept : MonoBehaviour
{
    public string player;
    public void yes()
    {
        GameObject.Find("client").GetComponent<client>().connectTo(player);
    }
}
