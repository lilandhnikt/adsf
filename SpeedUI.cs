using UnityEngine;
using TMPro;

public class SpeedUI : MonoBehaviour
{
    public Rigidbody playerRb;
    public TextMeshProUGUI speedText;

    void Update()
    {
        if (playerRb == null) return;

        float speed = playerRb.linearVelocity.magnitude;

        speedText.text = "Velocity: " + speed.ToString("F1");
    }
}