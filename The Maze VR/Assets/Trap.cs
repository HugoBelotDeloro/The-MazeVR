using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private StatusEffects effect;
    [SerializeField] private int duration;
    public int ID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<StatusEffectController>().AddStatusEffect(effect, duration);
            GameObject g = GameObject.Find("Game Player 1");
            g.GetComponent<addtrap>().remove.Add(ID);
            Destroy(gameObject);
        }
    }
}
