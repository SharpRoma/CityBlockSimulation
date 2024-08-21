using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // —сылка на игрока
    public float cameraSpeed = 2f; // —корость перемещени€ камеры
    public Vector3 offset; // —мещение камеры относительно игрока
    public float rotationSpeed = 1f; // —корость вращени€ камеры

    private float mouseX, mouseY; // ѕеременные дл€ хранени€ значений вращени€ мыши
    private float rotationX ;
    private float rotationY = 55; // ѕеременные дл€ хранени€ значений вращени€ осей

    private void LateUpdate()
    {
        // ѕолучаем позицию, к которой должна стремитьс€ камера
        Vector3 targetPosition = player.position + offset;

        // ¬ыполн€ем плавное перемещение камеры к позиции игрока
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

        // ѕолучаем значени€ вращени€ мыши
        rotationX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // ќграничиваем вертикальное вращение камеры до 90 градусов вверх и вниз
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        // ѕримен€ем вращение камеры
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}