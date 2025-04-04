using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float rotationSpeed = 5f; // Скорость поворота камеры
    public Vector3 offset; // Смещение камеры

    private void LateUpdate()
    {
        if (player != null)
        {
            // Поворачиваем камеру
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
            player.Rotate(0f, horizontal, 0f);

            // Позиционируем камеру за спиной у игрока
            transform.position = player.position + offset;

            // Сделаем камеру всегда смотрящей на игрока
            transform.LookAt(player);
        }
    }
}
