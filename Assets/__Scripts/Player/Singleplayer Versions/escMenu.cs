using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escMenu : MonoBehaviour
{
    public KeyCode escKey = KeyCode.Escape;
    public GameObject escMenuUI;

    private PlayerMovement playerMovement;
    private shootingWithRaycasts shootingWithRaycasts;
    private abilitiesHandler abilitiesHandler;
    private weaponsHandler weaponsHandler;
    void Update()
    {
        if (Input.GetKeyDown(escKey))
        {
            if (escMenuUI.active == true)
            {
                StopEscMenu();
            }
            else
            {
                StartEscMenu();
            }
        }
    }

    public void StartEscMenu()
    {
        DeactivatePlayerScripts();
        escMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void StopEscMenu()
    {
        ActivatePlayerScripts();
        escMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitToMenu()
    {

        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Menu Scene");
    }

    private void ActivatePlayerScripts()
    {
        if (playerMovement != null)
        {
            playerMovement.canMove = true;
        }
        if (shootingWithRaycasts != null)
        {
               shootingWithRaycasts.enabled = true;
        }
        if (abilitiesHandler != null)
        {
               abilitiesHandler.enabled = true;
        }
        if (weaponsHandler != null)
        {
               weaponsHandler.enabled = true;
        }
    }

    private void DeactivatePlayerScripts()
    {
        if (playerMovement != null && shootingWithRaycasts != null && weaponsHandler != null) 
        { 
            playerMovement.canMove = false;
            shootingWithRaycasts.enabled = false;
            weaponsHandler.enabled = false;
            return;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkObject>() != null)
            {
                if (player.GetComponent<NetworkObject>().IsLocalPlayer)
                {
                    playerMovement = player.GetComponent<PlayerMovement>();
                    shootingWithRaycasts = player.GetComponent<shootingWithRaycasts>();
                    abilitiesHandler = player.GetComponent<abilitiesHandler>();
                    weaponsHandler = null;
                    int numOfChildren = player.transform.childCount;
                    for (int i = 0; i < numOfChildren; i++)
                    {
                        if (player.transform.GetChild(i).gameObject.name == "Weapons Holder")
                        {
                            weaponsHandler = player.transform.GetChild(i).gameObject.GetComponent<weaponsHandler>();
                        }
                        break;
                    }
                    if (playerMovement != null)
                    {
                        playerMovement.canMove = false;
                    }
                    if (shootingWithRaycasts != null)
                    {
                        shootingWithRaycasts.enabled = false;
                    }
                    if (abilitiesHandler != null)
                    {
                        abilitiesHandler.enabled = false;
                    }
                    if (weaponsHandler != null)
                    {
                        weaponsHandler.enabled = false;
                    }
                    break;
                }
            }
        }
    }
}
