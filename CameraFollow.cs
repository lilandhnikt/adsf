using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Height")]
    public float height = 15f;
    public float minHeight = 5f;
    public float maxHeight = 30f;

    [Header("Zoom")]
    public float zoomSpeed = 10f;
    public float targetHeight;

    [Header("Smoothing")]
    public float smoothTime = 0.2f;

    private Vector3 velocity;

    void start()
    {
        targetHeight = height;
    }


    void Update()
    {
        HandleZoom();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x,
            height,
            target.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
    }

    void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y * 0.01f;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetHeight -= scroll * zoomSpeed;
            targetHeight = Mathf.Clamp(targetHeight, minHeight, maxHeight);
        }

        height = Mathf.Lerp(height, targetHeight, Time.deltaTime * 8f);

    }
}
