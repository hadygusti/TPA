using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public int maxHealth = 2000;
    public int health;

    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            Destroy(this.gameObject);
        } 
    }

    public void getShot(){
        health -= 20;
        healthBar.SetHealth(health);
    }
}
