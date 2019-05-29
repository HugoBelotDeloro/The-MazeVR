using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource introsource;
    public AudioSource loopsource;

    void Start()
    {
        introsource.Play();
    }


    void Update()
    {
        if (!introsource.isPlaying& !loopsource.isPlaying)
        {
            loopsource.Play();
        }
    }
}
