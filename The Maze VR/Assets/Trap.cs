using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private StatusEffects effect;
    [SerializeField] private int duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<StatusEffectController>().AddStatusEffect(effect, duration);
            Destroy(gameObject);
        }
    }
}
