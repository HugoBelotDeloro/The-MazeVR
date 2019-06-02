using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR : MonoBehaviour
{
    public InputField passfield;
    public InputField userfield;
    public GameObject client;
    public void load()
    {
        //Debug.Log("VR, username: " + userfield.text + ", password: " + passfield.text);
        var c = client.GetComponent<client>();
        c.me = userfield.text;
        c.S = "VR";
        c.login(passfield.text, userfield.text);
    }
}
;
