using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float attackRange = 2f;
    public LayerMask enemyLayer; // Слой врагов

    private CharacterStats characterStats;
    private Rigidbody rb;
   // private Animator animator;

    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody>();
     //   animator = GetComponentInChildren<Animator>();

        if (characterStats == null) Debug.LogError("CharacterStats компонент не найден!");
        if (rb == null) Debug.LogError("Rigidbody компонент не найден!");
    //    if (animator == null) Debug.LogError("Animator не найден!");
    }

    void Update()
    {
        if (characterStats.isDead) return;

        // Получаем направления движения
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Создаем вектор движения в локальных координатах
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Изменяем направление в зависимости от направления камеры
        if (direction.magnitude > 0)
        {
            // Преобразуем локальные координаты в глобальные
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
            forward.y = 0; // Обнуляем высоту
            right.y = 0; // Обнуляем высоту
            forward.Normalize();
            right.Normalize();
            direction = (forward * vertical + right * horizontal).normalized; // Новое направление движения
        }

        // Бег
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        /*
        // Анимации передвижения
        if (direction.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
            if (Input.GetKey(KeyCode.LeftShift))
                animator.SetBool("isRunning", true);
            else
                animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        */
        // Движение
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        // Поворот персонажа в сторону движения
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Атака
        if (Input.GetMouseButtonDown(0)) // Физическая атака
        {
            characterStats.Attack(DamageType.Physical, FindTarget());
        }
        else if (Input.GetMouseButtonDown(1)) // Магическая атака
        {
            characterStats.Attack(DamageType.Magical, FindTarget());
        }
    }

    private Transform FindTarget()
    {
        // Raycast для определения цели в радиусе атаки
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red, 1f);
            return hit.transform;
        }

        return null;
    }
}
