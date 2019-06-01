using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTRL : MonoBehaviour
{
    public InputField passfield;
    public InputField userfield;
    public GameObject client;
    public void load()
    {
        //Debug.Log("CTRL, username: " + userfield.text + ", password: " + passfield.text);
        var c = client.GetComponent<client>();
        c.me = userfield.text;
        c.S = "CTRL";
        c.login(passfield.text, userfield.text);
    }
}
