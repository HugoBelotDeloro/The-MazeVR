using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject parent;
    
    public void switching()
    {
        parent.GetComponent<Light>().enabled = !parent.GetComponent<Light>().enabled;
    }
}
