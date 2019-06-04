using System;
using UnityEngine;
using UnityEngine.UI;

public class ColdArea : MonoBehaviour
{
    private PlayerHealth _health;
    private Image _snowOverlay;
    private bool _active;
    private Equipment _equipment;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _equipment = other.GetComponentInChildren<Equipment>();
            if (!_equipment.HasItem(Equipment.ItemType.Clothes))
            {
                _active = true;
                _health = other.GetComponent<PlayerHealth>();
                _snowOverlay = other.transform.Find("HUD").Find("SnowOverlay").GetComponent<Image>();
                Color color = _snowOverlay.color;
                color.a = 255;
                _snowOverlay.color = color;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_active)
        {
            _health.Damage(5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _active)
        {
            _active = false;
            Color color = _snowOverlay.color;
            color.a = 0;
            _snowOverlay.color = color;
        }
    }
}
