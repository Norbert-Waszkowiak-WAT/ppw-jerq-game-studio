using UnityEngine;

public class Target : MonoBehaviour
{

    public float maxHealth = 150f;
    public float currentHealth = 150f;

    public healthBar healthBar;

    void Awake ()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
