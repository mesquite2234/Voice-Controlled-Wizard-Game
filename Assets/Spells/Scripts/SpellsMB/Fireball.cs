using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody rb;

    public Vector3 playerFwd;

    public float startSize;
    public float startSpeed;
    public float duration;
    public float damage;

    public static Vector3 globalGravity = new Vector3(0, -9.81f, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(playerFwd * startSpeed);

        rb.AddForce(ForwardToUp(playerFwd) * startSize / 3);

        Destroy(gameObject, duration);

        transform.localScale = new Vector3(startSize, startSize, startSize);
    }

    private void FixedUpdate()
    {
        rb.AddForce(globalGravity * 0.5f, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    Vector3 ForwardToUp(Vector3 fwd)
    {
        Vector3 right = Vector3.Cross(fwd, Vector3.up);
        Vector3 up = Vector3.Cross(right, fwd);
        return up;
    }
}
