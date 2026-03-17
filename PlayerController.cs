using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Speed Effect")]
    public ParticleSystem speedEffect;
    public float speedThreshold = 50f;

    public float acceleration = 10f;
    public float maxSpeed = 300f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 111f;

    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        RotateToMouse();
        HandleFire();
        HandleSpeedEffect();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }
    void HandleSpeedEffect()
{
    if (rb == null || speedEffect == null) return;

    float speed = rb.linearVelocity.magnitude;

    if (speed > speedThreshold)
    {
        if (!speedEffect.isPlaying)
        {
            speedEffect.Play();
        }
    }
    else
    {
        if (speedEffect.isPlaying)
        {
            speedEffect.Stop();
        }
    }
}
    void RotateToMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 mousePoint = ray.GetPoint(distance);

            Vector3 direction = mousePoint - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(targetRotation);
            }
        }
    }

    void HandleMovement()
    {
        if (Input.GetMouseButton(1)) // Right Mouse Button
        {
            Vector3 force = transform.forward * acceleration;
            rb.AddForce(force);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    void HandleFire()
{
    if (Input.GetMouseButtonDown(0))
    {
        RotateToMouse(); // ✅ Force latest rotation BEFORE firing

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }




}
}