using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealthManager : MonoBehaviour
{
    public int maxHealth = 80;
    public int health;

    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (health <= 0){
            Destroy(this.gameObject);
            if(QuestController.questIdx == 4 || QuestController.questIdx == 6)
            {
                QuestController.killCount++;
            }
        } 
    }

    public void getShot(int damage){
        health -= damage;
        healthBar.SetHealth(health);
    }


}
