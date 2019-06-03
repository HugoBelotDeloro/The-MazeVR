using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.gameObject.GetComponent<PlayerHealth>().Damage(1000);
            Destroy(gameObject);
        }
    }
}
