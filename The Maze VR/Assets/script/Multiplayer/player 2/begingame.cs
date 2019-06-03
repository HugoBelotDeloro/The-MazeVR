using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class begingame : MonoBehaviour
{
    public GameObject parent;

    public Canvas canvas;

    public InputField I3;

    public string code;

    public Camera c;
    
    // Start is called before the first frame update
    public void clicking()
    {
        Parser p = gameObject.AddComponent<Parser>();
        try
        {
            code = I3.text;
            p.codeToMap(code);
            c.GetComponent<Move>().activated = true;
            canvas.GetComponent<Canvas>().enabled = false;
            parent.GetComponent<placetrap>().enabled = true;
        }
        catch (Exception)
        {
            I3.text = "";
        }
    }
}
