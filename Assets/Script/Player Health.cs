using UnityEngine;
using UnityEngine.SceneManagement;  // Tambahkan ini untuk mengakses SceneManager

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Health maksimum
    private int currentHealth;   // Health saat ini
    private SpeedMovement movementPlayer;

    void Start()
    {
        currentHealth = maxHealth;  // Set health awal pemain
    }

    // Metode untuk mengurangi health pemain
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Pastikan health tidak kurang dari 0

        Debug.Log("Current Health: " + currentHealth);  // Untuk melihat health di konsol

        if (currentHealth <= 0)
        {
            Die();  // Panggil fungsi ketika health habis
        }
    }

    // Metode untuk menangani kematian pemain
    void Die()
    {
        Debug.Log("Player Died");
        
        // Nonaktifkan pergerakan pemain
        movementPlayer = FindObjectOfType<SpeedMovement>();
        movementPlayer.enabled = false;

        // Kembali ke main menu
        ReturnToMainMenu();
    }

    // Metode untuk kembali ke main menu
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");  // Ganti "MainMenu" dengan nama scene menu utama Anda
    }

    // Metode yang dipanggil saat pemain masuk collider dengan musuh
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))  // Pastikan tag enemy adalah "Enemy"
        {
            Die();  // Panggil metode untuk menangani kematian
        }
    }
}
