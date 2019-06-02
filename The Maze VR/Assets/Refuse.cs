using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refuse : MonoBehaviour
{
    public string player;
    // Start is called before the first frame update
    public void no()
    {
        GameObject.Find("popup").SetActive(false);
        GameObject.Find("Canvas").SetActive(true);
    }
}
