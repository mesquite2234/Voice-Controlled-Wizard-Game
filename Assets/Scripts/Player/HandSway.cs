using UnityEngine;

public class HandSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 0.05f;     // How much the gun moves
    public float swaySmoothness = 4f;    // How quickly it follows movement

    private Vector3 initialPosition;

    void Start()
    {
        // Store the original position
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Get mouse movement
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;

        // Target sway position
        Vector3 targetPosition = new Vector3(moveX, moveY, 0f);

        // Smoothly move towards the target position from the initial position
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            initialPosition + targetPosition,
            Time.deltaTime * swaySmoothness
        );
    }
}
