using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;
using WebSocketSharp;

public class abilitiesHandler : NetworkBehaviour
{
    public KeyCode ability1;
    public KeyCode ability2;
    public KeyCode ability3;
    

    public List<Ability> currentAbbilities = new List<Ability>();
    public List<float> abilitiesCooldowns = new List<float>();

    private Ability usedAbility;

    public Camera playerCamera;

    private GameObject wallBlueprint;

    public Material wallBlueprintMaterial;

    public shootingWithRaycasts shootingWithRaycastsInstance;
    public PlayerMovement playerMovementInstance;
    private struct WallRaisingData
    {
        public float maxDistance;
        public float width;
        public float height;
        public float length;
        public float speed;
    }

    private WallRaisingData wallRaisingData;

    public GameObject wallPrefab;

    public AudioSource audioSource;

    public AudioClip flashSound;
    public AudioClip superSpeedSound;

    private bool inSuperSpeed = false;

    private void Start()
    {
        if (!IsOwner) return;
        GameObject otherHolder = GameObject.Find("Other Holder");
        wallPrefab = otherHolder.transform.Find("WallPrefab").gameObject;

        Ability newAbility = new Ability();
        newAbility.name = "flash";
        newAbility.data = "distance:40;"; 
        newAbility.cooldown = (float) (1);
        newAbility.index = 0;
        currentAbbilities.Add(newAbility);
        abilitiesCooldowns.Add(newAbility.cooldown);

        Ability newAbility2 = new Ability();
        newAbility2.name = "wall raising";
        newAbility2.data = "maxDistance:50;width:5;height:3;length:2;speed:2;";
        newAbility2.cooldown = (float)(1);
        newAbility2.index = 1;
        currentAbbilities.Add(newAbility2);
        abilitiesCooldowns.Add(newAbility2.cooldown);

        Ability newAbility3 = new Ability();
        newAbility3.name = "super speed";
        newAbility3.data = "speedIncrease:60;duration:5;";
        newAbility3.cooldown = (float)(1);
        newAbility3.index = 2;
        currentAbbilities.Add(newAbility3);
        abilitiesCooldowns.Add(newAbility3.cooldown);

        SetWallRaisingData();
        wallBlueprint = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallBlueprint.GetComponent<BoxCollider>().enabled = false;
        wallBlueprint.transform.localScale = new Vector3(wallRaisingData.width, wallRaisingData.height, wallRaisingData.length);
        wallBlueprint.GetComponent<MeshRenderer>().material = wallBlueprintMaterial;
        wallBlueprint.layer = 8;
        wallBlueprint.active = false;
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(ability1))
        {
            if (abilitiesCooldowns[0] <= 0)
            {
                abilitiesCooldowns[0] = currentAbbilities[0].cooldown;
                DoAbility(currentAbbilities[0]);   
            }
        }
        if (Input.GetKeyDown(ability2))
        {
            if (abilitiesCooldowns[1] <= 0)
            {
                abilitiesCooldowns[1] = currentAbbilities[1].cooldown;
                DoAbility(currentAbbilities[1]);
            }
        }
        if (Input.GetKeyDown(ability3))
        {
            if (abilitiesCooldowns[2] <= 0)
            {
                abilitiesCooldowns[2] = currentAbbilities[2].cooldown;
                DoAbility(currentAbbilities[2]);
            }
        }

        if (usedAbility != null && usedAbility.name == "wall raising")
        {
            DisplayBlueprint();
            if (Input.GetMouseButtonDown(0))
            {
                wallBlueprint.active = false;
                usedAbility = null;
                wallBlueprint.active = false;
                SendWallRaisingToAllPlayers();
            }
        }

        HandleCooldowns();
    }

    void SendWallRaisingToAllPlayers()
    {
        FinishWallRaising(wallBlueprint.transform.position + new Vector3(0f, -wallRaisingData.height / 2, 0f), wallBlueprint.transform.rotation);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player != gameObject)
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    player.GetComponent<abilitiesHandler>().FinishWallRaisingClientRpc(wallBlueprint.transform.position + new Vector3(0f, -wallRaisingData.height / 2, 0f), wallBlueprint.transform.rotation);
                }
                else
                {
                    player.GetComponent<abilitiesHandler>().FinishWallRaisingServerRpc(wallBlueprint.transform.position + new Vector3(0f, -wallRaisingData.height / 2, 0f), wallBlueprint.transform.rotation);
                }
            }
        }
    }

    void SetWallRaisingData()
    {
        wallRaisingData.maxDistance = float.Parse(ExtractData(currentAbbilities[1].data, "maxDistance"));
        wallRaisingData.width = float.Parse(ExtractData(currentAbbilities[1].data, "width"));
        wallRaisingData.height = float.Parse(ExtractData(currentAbbilities[1].data, "height"));
        wallRaisingData.length = float.Parse(ExtractData(currentAbbilities[1].data, "length"));
        wallRaisingData.speed = float.Parse(ExtractData(currentAbbilities[1].data, "speed"));
    }

    void DisplayBlueprint()
    {
        wallBlueprint.active = true;
        RaycastHit hit;
        LayerMask layerMask = ~LayerMask.GetMask("Player", "Ability");
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, wallRaisingData.maxDistance, layerMask) && hit.transform != null)
        {
            wallBlueprint.transform.position = new Vector3(hit.point.x, hit.point.y + wallRaisingData.height / 2 + 0.01f, hit.point.z);
            wallBlueprint.transform.rotation = transform.rotation;
        }
    }

    [ClientRpc]
    void FinishWallRaisingClientRpc(Vector3 position, Quaternion rotation)
    {
        Debug.LogError("finish wall raising client rpc");
        GameObject wall = Instantiate(wallPrefab, position, rotation);
        wall.transform.localScale = new Vector3(wallRaisingData.width, 0, wallRaisingData.length);
        BoxCollider wallBoxCollider = wall.AddComponent<BoxCollider>();
        wall.active = true;
    }

    [ServerRpc(RequireOwnership = false)]
    void FinishWallRaisingServerRpc(Vector3 position, Quaternion rotation)
    {
        Debug.LogError("finish wall raising server rpc");
        GameObject wall = Instantiate(wallPrefab, position, rotation);
        wall.transform.localScale = new Vector3(wallRaisingData.width, 0, wallRaisingData.length);
        BoxCollider wallBoxCollider = wall.AddComponent<BoxCollider>();
        wall.active = true;
    }

    void FinishWallRaising(Vector3 position, Quaternion rotation)
    {
        Debug.LogError("finish wall raising");
        GameObject wall = Instantiate(wallPrefab, position, rotation);
        wall.transform.localScale = new Vector3(wallRaisingData.width, 0, wallRaisingData.length);
        BoxCollider wallBoxCollider = wall.AddComponent<BoxCollider>();
        wall.active = true;
    }

    void StartWallRaising(Ability ability)
    {
        if (usedAbility != ability)
        {
            shootingWithRaycastsInstance.disableShootingForShots += 1;
            usedAbility = ability;
        }
        else
        {
            shootingWithRaycastsInstance.disableShootingForShots -= 1;
            abilitiesCooldowns[ability.index] = 0;
            usedAbility = null;
            wallBlueprint.active = false;
        }
    }

    void HandleCooldowns()
    {
        for (int i = 0; i < abilitiesCooldowns.Count; i++)
        {
            if (abilitiesCooldowns[i] > 0)
            {
                abilitiesCooldowns[i] -= Time.deltaTime;
            }
        }
    }  
    
    void DoAbility(Ability ability)
    {
        if (ability.name == "flash")
        {
            Flash(ability);
        }
        else if (ability.name == "wall raising")
        {
            StartWallRaising(ability);
        }
        else if (ability.name == "super speed")
        {
            if (!inSuperSpeed)
            {
                StartCoroutine(SuperSpeed(ability));
            }
        }
    }

    IEnumerator SuperSpeed(Ability ability)
    {
        audioSource.PlayOneShot(superSpeedSound);
        float duration = float.Parse(ExtractData(ability.data, "duration"));
        float speedIncrease = float.Parse(ExtractData(ability.data, "speedIncrease"));
        playerMovementInstance.sprintSpeed *= (1 + speedIncrease / 100);
        inSuperSpeed = true;
        yield return new WaitForSeconds(duration);
        inSuperSpeed = false;
        playerMovementInstance.sprintSpeed /= (1 + speedIncrease / 100);
    }

    //"maxDistance:10;width:10;height:5;length:5;speed:2"

    
    void Flash(Ability ability)
    {
        audioSource.PlayOneShot(flashSound);
        float distance = float.Parse(ExtractData(ability.data, "distance"));
        Rigidbody rb = GetComponent<Rigidbody>();
        RaycastHit hit;
        //exclude player layer and ability layer
        LayerMask layerMask = ~LayerMask.GetMask("Player", "Ability");
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, layerMask) && hit.transform != null)
        {
            if (hit.distance < 1)
            {
                Debug.Log("distance < 1");
                return;
            }
            else
            {
                Debug.Log("start position: " + transform.position + "end position: " + (hit.point - transform.forward));
                rb.MovePosition(hit.point - transform.forward * 1f);
            }
            Debug.Log(hit.transform.name);
        }
        else
        {
            Debug.Log("start position: " + transform.position + "end position: " + (transform.position + transform.forward * distance));
            rb.MovePosition(transform.position + transform.forward * distance);
        }
        Debug.LogError("flash");
        
    }


    //"distance:5;"
    string ExtractData(string abilityData, string dataName)
    {
        string[] data = abilityData.Split(';');
        for (int i = 0; i < data.Length; i++)
        {
            string[] data2 = data[i].Split(':');
            if (data2[0] == dataName)
            {
                return data2[1];
            }
        }
        return "-1";
    }
}

public class Ability
{
    public string name;
    public float cooldown;
    public string data;
    public int index;
}
