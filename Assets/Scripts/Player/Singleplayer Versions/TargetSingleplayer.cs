using Unity.Netcode;
using UnityEngine;
using TMPro;

public class TargetSingleplayer : MonoBehaviour
{

    public float maxHealth = 150f;
    public float currentHealth = 150f;

    public bool canDie = true;

    public healthBar healthBar;

    public TMP_Text damageTaken;


    
    public void Awake ()
    {
       if (healthBar != null)
       {
           healthBar.SetMaxHealth(maxHealth);
           healthBar.SetHealth(maxHealth);
       }
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (damageTaken != null)
        {
            damageTaken.text = amount.ToString();
        }
    }

    void Die ()
    {
        if (!canDie)
        {
            currentHealth = maxHealth;
        } else
        {
            Destroy(gameObject);
        }
    }
}
