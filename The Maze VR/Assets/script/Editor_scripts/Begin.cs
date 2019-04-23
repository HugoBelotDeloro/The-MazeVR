using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Begin : MonoBehaviour
{
    public GameObject parent;

    public Button valid;

    public bool activated = false;

    public Canvas canvas;

    public InputField I1;

    public InputField I2;

    public int H;

    public int L;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begining());
    }

    // Update is called once per frame
    void Update()
    {
        valid.onClick.AddListener(OnClickThing);
    }

    IEnumerator Begining()
    {
        while (!activated)
        {
            yield return new WaitUntil((() => activated));
            try
            {
                L = Convert.ToInt32(I1.text);
                H = Convert.ToInt32(I2.text);
                parent.GetComponentInChildren<Move>().activated = true;
                canvas.GetComponent<Canvas>().enabled = false;
                parent.GetComponent<Edit>().enabled = true;
            }
            catch (Exception)
            {
                OnClickThing();
                I1.text = "";
                I2.text = "";
            }
        }
    }
    
    void OnClickThing()
    {
        activated = !activated;
    }
}
