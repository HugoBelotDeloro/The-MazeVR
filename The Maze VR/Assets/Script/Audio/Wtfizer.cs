using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Wtfizer : MonoBehaviour
{
    public float threeshold;
    public GameObject player;
    public float maxchorus;
    public float maxdisto;
    private AudioChorusFilter chorus;
    private AudioDistortionFilter disto;

    void Awake()
    {
        chorus = player.GetComponent<AudioChorusFilter>();
        disto = player.GetComponent<AudioDistortionFilter>();
    }
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position)< threeshold)
        {
            chorus.dryMix = maxchorus - Vector3.Distance(player.transform.position, transform.position)/threeshold*maxchorus;
            disto.distortionLevel = maxdisto - Vector3.Distance(player.transform.position, transform.position)/threeshold*maxdisto;
        }
    }
}
