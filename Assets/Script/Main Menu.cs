using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Panel untuk menampilkan Credit
    public GameObject creditPanel;

    // Method untuk tombol Start
    public void StartGame()
    {
        // Ganti ke scene utama, pastikan nama scene di Unity sesuai
        SceneManager.LoadScene("backupsave");
    }

    // Method untuk tombol Credits
    public void ShowCredits()
    {
        // Menampilkan panel credit
        creditPanel.SetActive(true);
    }

    // Method untuk tombol Back di panel Credits
    public void HideCredits()
    {
        // Menyembunyikan panel credit
        creditPanel.SetActive(false);
    }

    // Method untuk tombol Quit
    public void QuitGame()
    {
        // Menutup aplikasi
        Application.Quit();
        Debug.Log("Game exited");
    }
}
