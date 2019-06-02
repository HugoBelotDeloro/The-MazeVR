using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject g = GameObject.Find("Game Player 1");
            g.GetComponent<addtrap>().win = true;
        }
    }
}
