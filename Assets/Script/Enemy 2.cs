using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
  public int health = 100;

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
        Destroy(gameObject); // Menghancurkan objek enemy
        // Tambahkan efek visual atau suara di sini jika perlu
    }
}
