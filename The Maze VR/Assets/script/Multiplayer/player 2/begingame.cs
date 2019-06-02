using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class begingame : MonoBehaviour
{
    public GameObject parent;

    public Button valid;

    public bool activated = false;

    public Canvas canvas;

    public bool create;

    public InputField I3;

    public string code;
    
    private Parser p = new Parser();

    public Camera c;
    
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
                code = I3.text;
                p.codeToMap(code);
                c.GetComponent<Move>().activated = true;
                canvas.GetComponent<Canvas>().enabled = false;
                parent.GetComponent<placetrap>().enabled = true;
            }
            catch (Exception)
            {
                I3.text = "";
                OnClickThing();
            }
        }
    }
    
    void OnClickThing()
    {
        activated = !activated;
    }
}
