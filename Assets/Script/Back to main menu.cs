using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    // Nama scene menu utama yang ingin dituju
    public string mainMenuSceneName = "MainMenu"; 

    void Update()
    {
        // Mengecek jika tombol E ditekan
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Memindahkan ke scene menu utama
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
