using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Wtfizer : MonoBehaviour
{
    public float threeshold;
    private GameObject player;
    public float maxchorus;
    public float maxdisto;
    private AudioChorusFilter chorus;
    private AudioDistortionFilter disto;

    void Awake()
    {
        chorus = GameObject.Find("Player").GetComponent<AudioChorusFilter>();
        disto = GameObject.Find("Player").GetComponent<AudioDistortionFilter>();
    }
    void Update()
    {
        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position)< threeshold)
        {
            chorus.dryMix = maxchorus - Vector3.Distance(GameObject.Find("Player").transform.position, transform.position)/threeshold*maxchorus;
            disto.distortionLevel = maxdisto - Vector3.Distance(GameObject.Find("Player").transform.position, transform.position)/threeshold*maxdisto;
        }
    }
}
