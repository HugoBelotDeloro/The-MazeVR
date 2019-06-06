using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Begin : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    [SerializeField] private Canvas canvas;

    [SerializeField] private InputField I1;

    [SerializeField] private InputField I2;

    public int H;

    public int L;

    public bool create;

    [SerializeField] private InputField I3;

    public string code;

    [SerializeField] private bool ok;

    public void click()
    {
        try
        {
            ok = true;
            code = I3.text;
            if (code != "")
                create = false;
            else
            {
                L = Convert.ToInt32(I1.text);
                H = Convert.ToInt32(I2.text);
                if (L*H>=2)
                    create = true;
                else
                    ok = false;
            }
            if (ok)
            {
                parent.GetComponentInChildren<Move>().activated = true;
                canvas.GetComponent<Canvas>().enabled = false;
                parent.GetComponent<Edit>().enabled = true;
            }
            else
            {
                I1.text = "";
                I2.text = "";
                I3.text = "";
            }
        }
        catch (Exception)
        {
            I1.text = "";
            I2.text = "";
            I3.text = "";
        }
    }
}
