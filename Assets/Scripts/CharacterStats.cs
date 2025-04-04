using UnityEngine;
using UnityEngine.Events; // Для событий

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float baseDamage = 10f;
    public float magicMultiplier = 1.5f;

    public UnityEvent OnDeath; // Событие смерти
    public UnityEvent OnTakeDamage; // Событие получения урона
    public UnityEvent OnAttack; // Событие атаки

    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        // Инициализируем события, если они еще не созданы
        if (OnDeath == null) OnDeath = new UnityEvent();
        if (OnTakeDamage == null) OnTakeDamage = new UnityEvent();
        if (OnAttack == null) OnAttack = new UnityEvent();
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        if (isDead) return;

        currentHealth -= damage;
        OnTakeDamage.Invoke(); // Вызываем событие получения урона

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log(gameObject.name + " получил " + damage + " урона. HP: " + currentHealth);
    }

    public void Die()
    {
        isDead = true;
        OnDeath.Invoke(); // Вызываем событие смерти
    }

    public void Attack(DamageType damageType, Transform target)
    {
        if (isDead) return; // Нельзя атаковать, если мертв

        float damage = baseDamage;
        if (damageType == DamageType.Magical)
        {
            damage *= magicMultiplier;
        }

        OnAttack.Invoke(); // Вызываем событие атаки

        if (target != null)
        {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                targetStats.TakeDamage(damage, damageType);
            }
        }
    }
}

public enum DamageType
{
    Physical,
    Magical
}
