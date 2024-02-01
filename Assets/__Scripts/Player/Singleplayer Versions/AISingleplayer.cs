using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISingleplayer : MonoBehaviour
{

    public string state = "";
    public float timeFromLastSeeingPlayer = -1f;
    public float currentHp;
    public float maxHp;
    public int ammo;
    public int maxAmmo;
    public Vector3 lastSeenPlayerPosition;

    public string stateFromSeeingPlayer = "";
    public float stateFromSeeingPlayerPower = 0f;
    public string stateFromHP = "";
    public float stateFromHPPower = 0f;
    public string stateFromAmmo = "";
    public float stateFromAmmoPower = 0f;

    public GameObject player;
    public Camera AIcamera;

    public float timeToForgetPlayer = 5f;

    public AIMovement AImovement;

    private void Update()
    {
        if (state == "" || state != "")
        {
            state = GetNewState();
        }
        DoJobs();
        GetData();
    }

    void DoJobs()
    {
        if (state == "chase")
        {
            AImovement.moveToPosition = lastSeenPlayerPosition;
            AImovement.move = true;
        }
    }

    void GetData()
    {
        currentHp = GetComponent<TargetSingleplayer>().currentHealth;
        ammo = GetComponent<shootingWithRaycastsSingleplayer>().weaponsMagazines[GetComponent<shootingWithRaycastsSingleplayer>().gun.GetComponent<GunStats>().thisWeapon.weaponIndex];
        maxAmmo = GetComponent<shootingWithRaycastsSingleplayer>().gun.GetComponent<GunStats>().thisWeapon.magazineSize;
        maxHp = GetComponent<TargetSingleplayer>().maxHealth;
        //timeFromLastSeeingPlayer = 0 if AIcamera sees player;
        timeFromLastSeeingPlayer =  IsObjectVisible(player, AIcamera) ? 0f : timeFromLastSeeingPlayer + Time.deltaTime;
        if (IsObjectVisible(player, AIcamera))
        {
            lastSeenPlayerPosition = player.transform.position;

        }
    }

    public bool IsObjectVisible(GameObject player, Camera camera)
    {
        if (camera == null || player == null)
        {
            Debug.LogWarning("Camera or target object is not set.");
            return false;
        }

        // Calculate the camera's view frustum planes
        Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(camera);

        // Check if the object is within the camera's frustum
        if (GeometryUtility.TestPlanesAABB(cameraPlanes, player.GetComponent<Renderer>().bounds))
        {
            return true;
        }

        return false;
    }

    string GetNewState()
    {
        string newState = "";

        if (timeFromLastSeeingPlayer == -1f || timeFromLastSeeingPlayer > timeToForgetPlayer)
        {
            stateFromSeeingPlayer = "patrol";
            stateFromSeeingPlayerPower = 0f;
        }
        else if (timeFromLastSeeingPlayer >= 0f && timeFromLastSeeingPlayer < timeToForgetPlayer)
        {
            stateFromSeeingPlayer = "chase";
            stateFromSeeingPlayerPower = 1/((timeFromLastSeeingPlayer/timeToForgetPlayer)*timeToForgetPlayer);
            if (timeFromLastSeeingPlayer == 0f)
            {
                stateFromSeeingPlayerPower = 99f;
            }
        }
        
        if (currentHp == maxHp)
        {
            stateFromHP = "patrol";
            stateFromHPPower = 0f;
        }
        else
        {
            stateFromHP = "flee";
            stateFromHPPower = 0.1f/Mathf.Min((float)currentHp / (float)maxHp, 1f);
        }
        

        

        if (ammo == 0)
        {
            stateFromAmmo = "flee";
            stateFromAmmoPower = 100f;
        }
        else
        {
            stateFromAmmo = "patrol";
            stateFromAmmoPower = 0f;
        }
        /*else
        {
            stateFromAmmo = "attack";
            stateFromAmmoPower = ammo/maxAmmo;
        }*/

        if (stateFromSeeingPlayerPower > stateFromHPPower && stateFromSeeingPlayerPower > stateFromAmmoPower)
        {
            return stateFromSeeingPlayer;
        }
        else if (stateFromHPPower > stateFromSeeingPlayerPower && stateFromHPPower > stateFromAmmoPower)
        {
            return stateFromHP;
        }
        else if (stateFromAmmoPower > stateFromSeeingPlayerPower && stateFromAmmoPower > stateFromHPPower)
        {
            return stateFromAmmo;
        }
        else
        {
            return "patrol";
        }
    }
}
