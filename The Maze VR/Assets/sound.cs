using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioSource asrc;
    public AudioClip snd;
    public void play()
    {
        asrc.PlayOneShot(snd);
    }
}
