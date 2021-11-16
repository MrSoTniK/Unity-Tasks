using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageButton : HealthChangerButton
{
    [SerializeField] private int _damage;

    protected override int ValueOfChange => _damage;
    protected override CheckMethod VerificationHP => Player.CheckDamagePossibility;

    protected override IEnumerator ChangeHP(int valueOfChange)
    {
        CoroutineIsWorking?.Invoke(true);
        float currentHealth = Player.Health;
        float targetHealth = currentHealth - valueOfChange;

        while (currentHealth > targetHealth)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, SpeedOfChange * Time.deltaTime);
            currentHealth = Mathf.Clamp(currentHealth, 0, Player.MaxHealth);
            HealthChanged?.Invoke(currentHealth);

            if (currentHealth == targetHealth)
            {
                Player.TakeDamage(valueOfChange);
                CoroutineIsWorking?.Invoke(false);
            }

            yield return new WaitForEndOfFrame();
        }
    }   
}
