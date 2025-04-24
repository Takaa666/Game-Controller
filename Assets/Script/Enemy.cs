using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject Musuh1;

    [Header("Loot")]
    public List<EnemyDrop> drop = new List<EnemyDrop>();

    // Metode untuk mengurangi kesehatan enemy
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Cek apakah kesehatan enemy sudah habis
        if (health <= 0)
        {
            Die();
        }
    }

    // Metode untuk menghancurkan enemy
    void Die()
    {
        foreach(EnemyDrop enemyDrop in drop)
        {
            if(Random.Range(0f, 100f) <= enemyDrop.dropChance)
            {
                InstantiateLoot(enemyDrop.itemPrefab);
            }
            break;
        }
        Musuh1.SetActive(false); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Die();
        }
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
            droppedLoot.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
