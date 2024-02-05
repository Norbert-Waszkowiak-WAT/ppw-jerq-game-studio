using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class inConsoleComputer : MonoBehaviour
{

    public float craneMin;
    public float craneMax;

    public TMP_Text distanceText;

    private void Awake()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        distanceText.text = DistanceToInt().ToString();
    }

    int DistanceToInt()
    {
        //distance between craneMin and craneMax return form 1 to 5 
        float distance = CoordinatesToDistance(ZFromFile());
        float delta = (craneMax - craneMin) / 5;
        return (int)Mathf.Floor(distance / delta);
    }

    float ZFromFile()
    {
        return 8;
        if (File.Exists(Application.persistentDataPath + "/shootingRangeData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/shootingRangeData.json");
            ShootingRangeData data = JsonUtility.FromJson<ShootingRangeData>(json);
            Debug.Log(data.botZPosition);
            return data.botZPosition;
        }
        Debug.Log("No save file found");
        return 0;
    }
    float CoordinatesToDistance(float z)
    {
        float delta = z - craneMin;
        return delta / (craneMax - craneMin);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Training Shooting Scene");
        }
    }
}
