using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 2f;
    public Vector3 offset;
    public float rotationSpeed = 1f;

    private float mouseX, mouseY;
    private float rotationX ;
    private float rotationY = 55;

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

        rotationX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
