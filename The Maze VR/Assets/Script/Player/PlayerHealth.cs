﻿using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health;
    private const int MaxHealth = 1000;
    private int _framesSinceLastHit;
    private const int FramesToRegen = 100;
    private const int HealthPerFrame = 1;
    public SpriteRenderer BloodOverlay;
    
    
    void Start()
    {
        _health = MaxHealth;
        _framesSinceLastHit = FramesToRegen;
        BloodOverlay.enabled = true;
        BloodOverlay.size = new Vector2(Screen.height, Screen.width);
        
    }

    void FixedUpdate()
    {
        if (_health < MaxHealth && _framesSinceLastHit > FramesToRegen)
        {
            _health += HealthPerFrame;
            if (_health >= MaxHealth)
            {
                _health = MaxHealth;
                BloodOverlay.enabled = false;
            }
            Color blood = BloodOverlay.color;
            blood.a = (float) (MaxHealth - _health) / MaxHealth;
            BloodOverlay.color = blood;
        }
        else
        {
            _framesSinceLastHit++;
        }
    }

    public void Damage(int damage)
    {
        _health -= damage;
    }
}