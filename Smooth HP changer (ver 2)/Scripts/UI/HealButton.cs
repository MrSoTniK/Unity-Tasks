using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : HealthChangerButton
{
    [SerializeField] private int healing;

    protected override int ValueOfChange => healing;

    protected override IEnumerator ChangeHP(int valueOfChange)
    {
        if(Player.Health < Player.MaxHealth) 
        {
            CoroutineIsWorking?.Invoke(true);
            float currentHealth = Player.Health;
            float targetHealth = currentHealth + valueOfChange;

            while (currentHealth < targetHealth)
            {
                currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, SpeedOfChange * Time.deltaTime);
                currentHealth = Mathf.Clamp(currentHealth, 0, Player.MaxHealth);
                HealthChanged?.Invoke(currentHealth);

                if (currentHealth == targetHealth)
                {
                    Player.TakeHeal(valueOfChange);
                    CoroutineIsWorking?.Invoke(false);
                }

                yield return new WaitForEndOfFrame();
            }
        }      
    }
}