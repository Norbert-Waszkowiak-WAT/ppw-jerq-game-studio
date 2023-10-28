using UnityEngine;
using UnityEngine.SceneManagement;

public class escMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu Scene");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
