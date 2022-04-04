using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : MonoBehaviour
{   
    // Health
    public int maxHealth = 2000, health;

    // Canvas
    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetShot(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }
}
