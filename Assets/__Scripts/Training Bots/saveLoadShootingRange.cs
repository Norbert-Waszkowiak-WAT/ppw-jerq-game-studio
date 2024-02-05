using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class saveLoadShootingRange : MonoBehaviour
{

    public GameObject player;
    public GameObject bot;
    public float craneMin;
    public float craneMax;
    private void Awake()
    {
        LoadShootingRange();
    }

    public void LoadShootingRange()
    {
        if(File.Exists(Application.persistentDataPath + "/shootingRangeData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/shootingRangeData.json");
            ShootingRangeData data = JsonUtility.FromJson<ShootingRangeData>(json);
            player.transform.position = data.playerPosition;
            player.transform.rotation = data.playerRotation;
            float botZPozition = craneMin + ((60 - data.botZPosition)/10 - 1) * (craneMax - craneMin) / 4;
            bot.transform.position = new Vector3(bot.transform.position.x,bot.transform.position.y, botZPozition);
        }
        else
        {
            Debug.Log("No save file found");
        }
    }

    public void SaveShootingRange()
    {
        float botZPosition = 50;
        if(File.Exists(Application.persistentDataPath + "/shootingRangeData.json"))
        {
            botZPosition = JsonUtility.FromJson<ShootingRangeData>(File.ReadAllText(Application.persistentDataPath + "/shootingRangeData.json")).botZPosition;
            File.Delete(Application.persistentDataPath + "/shootingRangeData.json");
        }
        ShootingRangeData data = new ShootingRangeData();
        data.playerPosition = player.transform.position;
        data.playerRotation = player.transform.rotation;
        data.botZPosition = botZPosition;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/shootingRangeData.json", json);
    }
}

public class ShootingRangeData
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float botZPosition;
}
