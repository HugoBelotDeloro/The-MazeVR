using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffects
{
    POWEROFF,
    POISON,
    SLOW
}

public abstract class StatusEffect
{
    protected int duration;
    public abstract void StatusEffectStart();
    protected abstract void StatusEffectUpdate();
    public abstract void StatusEffectEnd();

    public bool Update()
    {
        duration--;
        StatusEffectUpdate();
        return duration > 0;
    }
}

public class PowerOff : StatusEffect
{
    private Equipment equipment;
    private Light lamp;

    public PowerOff(Equipment equipment, Light lamp, int duration)
    {
        this.equipment = equipment;
        this.lamp = lamp;
        this.duration = duration;
    }

    public override void StatusEffectEnd()
    {
        if (equipment.HasItem(Equipment.ItemType.Light))
        {
            lamp.enabled = true;
        }
    }

    public override void StatusEffectStart()
    {
        if (lamp.enabled)
        {
            lamp.enabled = false;
        }
    }

    protected override void StatusEffectUpdate()
    {
        if (lamp.enabled)
        {
            lamp.enabled = false;
        }
    }
}

public class Poison : StatusEffect
{
    private PlayerHealth health;

    public Poison(PlayerHealth health, int duration)
    {
        this.health = health;
        this.duration = duration;
    }

    public override void StatusEffectEnd()
    {

    }

    public override void StatusEffectStart()
    {

    }

    protected override void StatusEffectUpdate()
    {
        health.Damage(1);

    }
}

public class Slow : StatusEffect
{
    private PlayerMovement movement;

    public Slow(PlayerMovement movement, int duration)
    {
        this.movement = movement;
        this.duration = duration;
    }

    public override void StatusEffectEnd()
    {
        movement.SetSpeed(movement.GetSpeed() * 2);
    }

    public override void StatusEffectStart()
    {
        movement.SetSpeed(movement.GetSpeed() * 0.5f);
    }

    protected override void StatusEffectUpdate()
    {
        
    }
}