using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float fireDamage = 20f;
    public float plasmaDamage = 25f;
    public float rateOfFire1 = 1f; //Fire fire rate
    public float rateOfFire2 = 0.5f; //Shock fire rate
    public float coins = 0f;
    public float experience = 0f;
    public int level = 1;
    public float increasePerLevel = 1.02f;
    public CanvasManager canvasManager;

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        canvasManager.GameLost();
        //Destroy(gameObject);
        //Debug.Log("YOU'RE DEAD");
    }

    public void GainExperience(float experieneGain)
    {
        experience = experience + experieneGain;
        if(experience % 100 == 0 || experience / level > 100)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level = level + 1;
        maxHealth = maxHealth * increasePerLevel;
        fireDamage = fireDamage * increasePerLevel;
        plasmaDamage = plasmaDamage * increasePerLevel;
        currentHealth = maxHealth;
    }

    public void Heal()
    {
        float r = Random.Range(20, 60);
        currentHealth = currentHealth + r;
    }

    public void AddGold()
    {
        float r = Random.Range(0, 150);
        coins = coins + r;
    }
}
