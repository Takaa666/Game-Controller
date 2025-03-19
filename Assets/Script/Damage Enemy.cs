using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;  // Jumlah damage yang diberikan kepada pemain
    public string playerTag = "Player";  // Tag yang digunakan untuk mengenali pemain

    // Ketika musuh menyentuh pemain (menggunakan trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Periksa apakah objek yang disentuh memiliki tag pemain
        if (collision.CompareTag(playerTag))
        {
            // Ambil komponen PlayerHealth dari objek pemain
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // Jika pemain memiliki komponen PlayerHealth, kurangi health pemain
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
