using System;
using UnityEngine;

public class ColdArea : MonoBehaviour
{
    private PlayerHealth _health;
    private bool _active;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _active = true;
            _health = other.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_active)
        {
            _health.Damage(10);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _active = false;
        }
    }
}
