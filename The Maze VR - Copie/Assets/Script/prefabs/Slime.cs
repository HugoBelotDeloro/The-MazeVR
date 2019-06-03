using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private float timer;

    private bool sleep;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Wait()
    {
        sleep = true;
        yield return new WaitForSeconds(15f);
        sleep = false;
    }
}
