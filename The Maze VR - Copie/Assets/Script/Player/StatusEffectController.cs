using System.Collections.Generic;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    List<StatusEffect> statusEffects;

    [SerializeField] PlayerHealth health;
    [SerializeField] PlayerMovement movement;
    [SerializeField] Light lamp;
    [SerializeField] Equipment equipment;

    private void Start()
    {
        statusEffects = new List<StatusEffect>();
        StatusEffectToGenerator.Add(StatusEffects.POISON, PoisonCreator);
        StatusEffectToGenerator.Add(StatusEffects.SLOW, SlowCreator);
        StatusEffectToGenerator.Add(StatusEffects.POWEROFF, PowerOffCreator);
    }

    public void AddStatusEffect(StatusEffects type, int duration)
    {
        StatusEffect effect = StatusEffectToGenerator[type](duration);
        statusEffects.Add(effect);
        effect.StatusEffectStart();
    }

    private void Update()
    {
        int length = statusEffects.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            StatusEffect effect = statusEffects[i];
            if (!effect.Update())
            {
                effect.StatusEffectEnd();
                statusEffects.RemoveAt(i);
            }
        }
    }

    private delegate StatusEffect StatusEffectGenerator(int duration);

    private StatusEffect PoisonCreator(int duration)
    {
        return new Poison(health, duration);
    }

    private StatusEffect SlowCreator(int duration)
    {
        return new Slow(movement, duration);
    }

    private StatusEffect PowerOffCreator(int duration)
    {
        return new PowerOff(equipment, lamp, duration);
    }

    private static Dictionary<StatusEffects, StatusEffectGenerator> StatusEffectToGenerator = new Dictionary<StatusEffects, StatusEffectGenerator>();
}