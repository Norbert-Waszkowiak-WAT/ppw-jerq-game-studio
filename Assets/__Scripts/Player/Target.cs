using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Target : NetworkBehaviour
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

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRpc (float amount)
    {
        LogWriter.WriteLog("Damage taken server rpc: " + amount);
        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0f)
        {
            Die();
        }

        if (damageTaken != null)
        {
            damageTaken.text = amount.ToString();
        }
    }

    [ClientRpc]
    public void TakeDamageClientRpc(float amount)
    {
        LogWriter.WriteLog("Damage taken client rpc: " + amount);
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
        LogWriter.WriteLog("Die");
        if (!canDie)
        {
            LogWriter.WriteLog("Can't die");
            currentHealth = maxHealth;
        } else
        {
            NetworkObject.Destroy(gameObject);
        }
    }
}
