using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health;
    private const int MaxHealth = 1000;
    private int _framesSinceLastHit;
    private const int FramesToRegen = 100;
    private const int HealthPerFrame = 1;
    [SerializeField]private SpriteRenderer BloodOverlay;
    [SerializeField]private GameObject deathScreen;
    
    
    void Start()
    {
        _health = MaxHealth;
        _framesSinceLastHit = FramesToRegen;
        BloodOverlay.enabled = true;
        
    }

    void FixedUpdate()
    {
        if (_health < MaxHealth && _framesSinceLastHit >= FramesToRegen && _health > 0)
        {
            _health += HealthPerFrame;
            if (_health >= MaxHealth)
            {
                _health = MaxHealth;
                BloodOverlay.enabled = false;
            }
            UpdateOverlay();
        }
        else if (_framesSinceLastHit < FramesToRegen)
        {
            _framesSinceLastHit++;
        }
    }

    private void UpdateOverlay()
    {
        Color blood = BloodOverlay.color;
        blood.a = (float) (MaxHealth - _health) / MaxHealth;
        BloodOverlay.color = blood;
    }

    public void Damage(int damage)
    {
        _health -= damage;
        _framesSinceLastHit = 0;
        UpdateOverlay();
        BloodOverlay.enabled = true;
        if (_health <= 0)
        {
            PrintDeathScreen();
        }
    }

    private void PrintDeathScreen()
    {
        gameObject.GetComponentInParent<PlayerMovement>().enabled = false;
        deathScreen.SetActive(true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        while (Input.GetAxis("Action") <= 0)
        {
            yield return new WaitForSecondsRealtime(0.05f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        GameManager.Instance.ResetScene();
    }
}