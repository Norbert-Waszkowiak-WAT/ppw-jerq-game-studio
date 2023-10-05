using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Target : NetworkBehaviour
{

    public float maxHealth = 150f;
    public float currentHealth = 150f;

    public healthBar healthBar;
    
    public void Awake ()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(maxHealth);
        }
        
    }
    

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        if (healthBar != null)  healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        NetworkObject.Destroy(gameObject);
    }
}
