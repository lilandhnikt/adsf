using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float height = 15f;
    public float smoothTime = 0.2f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        // Keep camera directly above player
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            height,
            target.position.z
        );

        if ((transform.position - desiredPosition).sqrMagnitude < 0.001f)
        return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
    }

    
}