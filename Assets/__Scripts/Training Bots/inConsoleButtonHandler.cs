using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class inConsoleButtonHandler : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;    
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;

    public TMP_Text distanceText;

    private void Awake()
    {
        LoadDistance();
    }

    private void LoadDistance()
    {
        if (File.Exists(Application.persistentDataPath + "/shootingRangeData.json"))
        {
            Debug.Log("Loading distance");
            ShootingRangeData data = JsonUtility.FromJson<ShootingRangeData>(File.ReadAllText(Application.persistentDataPath + "/shootingRangeData.json"));
            float distance = data.botZPosition;
            Debug.Log("Distance: " + distance);
            distanceText.text = distance.ToString();
        }
        else
        {
            Debug.Log("No save file found");
            distanceText.text = "50";
        }
    }

    private void SaveDistance(float distance)
    {
        Vector3 playerPosition = new Vector3(18.9f, 1.41f, 14.5f);
        Quaternion playerRotation = new Quaternion(0, 0, 0, 0);

        if (File.Exists(Application.persistentDataPath + "/shootingRangeData.json"))
        {
            ShootingRangeData data = JsonUtility.FromJson<ShootingRangeData>(File.ReadAllText(Application.persistentDataPath + "/shootingRangeData.json"));
            playerPosition = data.playerPosition;
            playerRotation = data.playerRotation;
            File.Delete(Application.persistentDataPath + "/shootingRangeData.json");
        }
        ShootingRangeData newData = new ShootingRangeData();
        newData.playerPosition = playerPosition;
        newData.playerRotation = playerRotation;
        newData.botZPosition = distance;
        string json = JsonUtility.ToJson(newData);
        File.WriteAllText(Application.persistentDataPath + "/shootingRangeData.json", json);
    }

    private IEnumerator ButtonAnimation(GameObject button)
    {
        button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y - 0.01f, button.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + 0.01f, button.transform.position.z);
    }

    public void Button1()
    {
        StartCoroutine(ButtonAnimation(button1));
        SaveDistance(10);
        distanceText.text = "10";
    }

    public void Button2()
    {
        StartCoroutine(ButtonAnimation(button2));
        SaveDistance(20);
        distanceText.text = "20";
    }

    public void Button3()
    {
        StartCoroutine(ButtonAnimation(button3));
        SaveDistance(30);
        distanceText.text = "30";
    }

    public void Button4()
    {
        StartCoroutine(ButtonAnimation(button4));
        SaveDistance(40);
        distanceText.text = "40";
    }

    public void Button5()
    {
        StartCoroutine(ButtonAnimation(button5));
        SaveDistance(50);
        distanceText.text = "50";
    }

}
