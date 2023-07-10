using UnityEngine;

public class TimeController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;

    private bool isTimeStopped = false;
    private float originalFixedDeltaTime;

    void Start()
    {
        player = GameObject.Find("PlayerController");
        playerRb = player.GetComponent<Rigidbody>();
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isTimeStopped)
            {
                ResumeTime();
            }
            else
            {
                StopTime();
            }
        }
    }

    void StopTime()
    {
        isTimeStopped = true;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb != playerRb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Imposta i rigidbody degli altri oggetti come kinematic
            }
        }
    }

    void ResumeTime()
    {
        isTimeStopped = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb != playerRb)
            {
                rb.isKinematic = false; // Ripristina i rigidbody degli altri oggetti come non kinematic
            }
        }
    }
}
