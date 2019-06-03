using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.name == "Player")
        {
            Debug.Log("The monster has found the player");
            Destroy(gameObject);
        }
    }
}
