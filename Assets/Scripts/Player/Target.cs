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

    [ClientRpc]
    public void TakeDamageClientRpc (float amount)
    {
        Debug.LogError("Taking damage");
        currentHealth -= amount;
        Debug.LogError(currentHealth);
        Debug.LogError(amount);

        if (healthBar != null)
        {
            Debug.LogError("healthBar != null");
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0f)
        {
            Debug.LogError("currentHealth <= 0f");
            Die();
        }
    }

    void Die ()
    {
        NetworkObject.Destroy(gameObject);
    }
}
