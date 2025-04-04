using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator animator;
    public CharacterStats characterStats; // Перетащите сюда CharacterStats вашего персонажа

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterStats.OnDeath.AddListener(PlayDeathAnimation); // Подписка на событие смерти
        characterStats.OnTakeDamage.AddListener(PlayHitAnimation); // Подписка на событие получения урона
        characterStats.OnAttack.AddListener(PlayAttackAnimation); // Подписка на событие атаки
    }

    void PlayDeathAnimation()
    {
        animator.SetTrigger("Die");
    }

    void PlayHitAnimation()
    {
        animator.SetTrigger("Hit");
    }

    void PlayAttackAnimation()
    {
        // Здесь нужно определить, какая анимация атаки должна проигрываться
        // Например, можно передавать тип атаки (DamageType) через событие

        animator.SetTrigger("Attack"); // Или "MagicAttack"
    }
}