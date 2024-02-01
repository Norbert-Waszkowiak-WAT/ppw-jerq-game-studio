using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tmpSceneChnage : MonoBehaviour
{
    public void ChangeToMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}
