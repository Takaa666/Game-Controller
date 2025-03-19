using NodeCanvas.Framework;  // Namespace untuk NodeCanvas
using ParadoxNotion.Design;  // Untuk dekorasi node
using UnityEngine;

[Category("Custom Tasks")]
public class IdleAction : ActionTask<Transform>  // Inherits dari ActionTask untuk task behavior
{
    public BBParameter<string> stateName;  // BlackBoard parameter untuk memasukkan nama state animasi
    private Animator animator;  // Animator component

    protected override string OnInit()
    {
        // Mengambil komponen Animator dari object yang ditargetkan (target Transform)
        animator = agent.GetComponent<Animator>();

        // Cek jika Animator ditemukan
        if (animator == null)
        {
            return "No Animator found on the GameObject.";
        }

        Debug.Log("Idle Task Initialized.");
        return null;  // Return null berarti tidak ada error
    }

    protected override void OnExecute()
    {
        // Memulai task
        Debug.Log("Starting Idle Task...");
        EndAction(false);  // Membiarkan task berjalan terus tanpa selesai
    }

    protected override void OnUpdate()
    {
        // Task akan terus diupdate setiap frame
        PerformIdleAction();
    }

    void PerformIdleAction()
    {
        // Cek apakah animator valid dan stateName sudah diisi
        if (animator != null && !string.IsNullOrEmpty(stateName.value))
        {
            // Mainkan animasi berdasarkan state name yang dimasukkan
            animator.Play(stateName.value);
            Debug.Log($"Playing animation state: {stateName.value}");
        }
        else
        {
            Debug.LogWarning("Animator or state name not set.");
        }
    }

    protected override void OnStop()
    {
        // Method ini dipanggil jika task dihentikan oleh tree
        Debug.Log("Idle Task Stopped.");
    }

    protected override void OnPause()
    {
        // Method ini dipanggil saat task dipause
        Debug.Log("Idle Task Paused.");
    }
}
