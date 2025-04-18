using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject Musuh1;

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
        Musuh1.SetActive(false); // Menghancurkan objek enemy
        // Tambahkan efek visual atau suara di sini jika perlu
    }
}
