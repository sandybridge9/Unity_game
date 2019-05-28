using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    public float expYield = 10f;
    public string type = "";

    public GameObject enemyToSpawn;
    public GameObject hpDrop;
    public GameObject coinDrop;
    public bool isDead = false;
    Player player;

    void Start()
    {
        player = PlayerManager.instance.player.GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            Transform lastPosition = transform;
            player.GainExperience(expYield);
            isDead = true;
            Destroy(gameObject, 3f);
            //DropItem(lastPosition);
            //Die();
        }
    }

    void Die()
    {
        //Transform lastPosition = transform;
        //player.GainExperience(expYield);
        //Destroy(gameObject, 3f);
        //DropItem(lastPosition);
    }

    void DropItem(Transform position)
    {
        int r = Random.Range(0, 10);
        if(r > 0.7)
        {
            Instantiate(hpDrop, position);
        }
        else if(r <= 0.7)
        {
            Instantiate(coinDrop, position);
        }
    }

    private void OnDestroy() //called, when enemy will be destroyed
    {
        float r = Random.Range(0, 1);
        if (r > 0.75)
        {
            //Instantiate(hpDrop, transform.position, hpDrop.transform.rotation);
        }
        else if (r <= 0.75)
        {
           // Instantiate(coinDrop, transform.position, coinDrop.transform.rotation);
        }
        PlayerManager.instance.deadCount = PlayerManager.instance.deadCount + 1;
        Debug.Log(PlayerManager.instance.deadCount);
        for (int i = 0; i < PlayerManager.instance.deadCount + 1; i++)
        {
            Instantiate(enemyToSpawn, new Vector3(transform.position.x + i + 2, transform.position.y, transform.position.z + i + 2), enemyToSpawn.transform.rotation);
        }
    }
}
