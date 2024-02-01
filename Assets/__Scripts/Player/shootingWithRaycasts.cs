
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class shootingWithRaycasts : NetworkBehaviour
{
    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    public ParticleSystem impactEffect;

    public TrailRenderer tracerEffect;
    public float damage = 10f;
    public float range = float.MaxValue;

    public Camera fpsCam;

    public float fireRate = 15f;

    public float headshotMultiplier;
    public float legshotMultiplier;

    public GameObject gun; 

    public string shootButton = "Fire1";
    public KeyCode reloadButton = KeyCode.R;

    public List<bool> weaponsCanShoot = new List<bool>();
    public List<int> weaponsMagazines = new List<int>();
    public bool reloading = false;

    public List<GameObject> players = new List<GameObject>();

    public int disableShootingForShots = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(shootButton) && !reloading && gun != null)
        {
            LogWriter.WriteLog("shoot button pressed");
            if (weaponsCanShoot[gun.GetComponent<GunStats>().thisWeapon.weaponIndex])
            {
                if (!(weaponsMagazines[gun.GetComponent<GunStats>().thisWeapon.weaponIndex] <= 0))
                {
                    LogWriter.WriteLog("shoot");
                    Shoot();
                    weaponsMagazines[gun.GetComponent<GunStats>().thisWeapon.weaponIndex]--;
                    weaponsCanShoot[gun.GetComponent<GunStats>().thisWeapon.weaponIndex] = false;
                    StartCoroutine(ShootDelay());
                }
                else
                {
                    LogWriter.WriteLog("magazine empty");
                }
            }
            else
            {
                LogWriter.WriteLog("shoot cooldown");
            }
        }

        if (Input.GetKeyDown(reloadButton) && !reloading)
        {
            StartCoroutine(Reloading());
            LogWriter.WriteLog("reload");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.transform.GetComponent<Target>().TakeDamageServerRpc(damage);
            LogWriter.WriteLog("self damage");
        }
    }

    public override void OnNetworkSpawn()
    {
        LogWriter.WriteLog("OnNetworkSpawn");
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in allPlayers)
        {

            //&& player != myself
            if (!players.Contains(player) && player != transform.gameObject)
            {
                players.Add(player);
            }
            else
            {
                continue;
            }
        }
    }

    IEnumerator ShootDelay()
    {
        int weaponIndex = gun.GetComponent<GunStats>().thisWeapon.weaponIndex;
        yield return new WaitForSeconds(1f / fireRate);
        weaponsCanShoot[weaponIndex] = true;
    }

    IEnumerator Reloading()
    {
        int weaponIndex = gun.GetComponent<GunStats>().thisWeapon.weaponIndex;
        reloading = true;
        yield return new WaitForSeconds(gun.GetComponent<GunStats>().thisWeapon.reloadSpeed);
        reloading = false;
        weaponsMagazines[weaponIndex] = gun.GetComponent<GunStats>().thisWeapon.magazineSize;
        LogWriter.WriteLog("reload done");
    }

    public void WaponChanged()
    {
        LogWriter.WriteLog("WaponChanged");
        int weaponIndex = gun.GetComponent<GunStats>().thisWeapon.weaponIndex;
        if (weaponsCanShoot.Count < weaponIndex + 1)
        {
            while (weaponsCanShoot.Count < weaponIndex + 1)
            {
                weaponsCanShoot.Add(true);
                weaponsMagazines.Add(-1);
            }
        }
        if (weaponsMagazines[weaponIndex] == -1)
        {
            weaponsMagazines[weaponIndex] = gun.GetComponent<GunStats>().thisWeapon.magazineSize;
        }
    }

    void Shoot()
    {
        if (disableShootingForShots > 0)
        {
            disableShootingForShots--;
            return;
        }
        gun.GetComponent<Fire>().fired = true;
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Create a raycast
        RaycastHit hit;
        Vector3 endPosition;

        bool hitSomething = false;

        //int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform == transform)
            {
                return;
            }
            Debug.Log(hit.collider.name);
            Hitbox hittedHitBox = hit.collider.gameObject.GetComponent<Hitbox>();
            float damageMultiplier = 1f;
            if (hittedHitBox != null)
            {
                if (hittedHitBox.hitboxType == "head")
                {
                    damageMultiplier = headshotMultiplier;
                    Debug.LogError("headshot");
                    LogWriter.WriteLog("headshot");
                }
                else if (hittedHitBox.hitboxType == "leg" || hittedHitBox.hitboxType == "arm")
                {
                    damageMultiplier = legshotMultiplier;
                    Debug.LogError("legshot");
                    LogWriter.WriteLog("legshot");
                }
                else
                {
                    Debug.LogError("bodyshot");
                    LogWriter.WriteLog("bodyshot");
                }
            }
            hitSomething = true;
            Debug.Log(hit.transform.name);
            endPosition = hit.point;

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    target.TakeDamageClientRpc(Mathf.Round(damage * damageMultiplier));
                }
                else
                {
                    target.TakeDamageServerRpc(Mathf.Round(damage * damageMultiplier));
                }
            }
        }
        else
        {
            endPosition = fpsCam.transform.position + (fpsCam.transform.forward * range);
        }

        if (tracerEffect != null)
        {
            TrailRenderer trail = Instantiate(tracerEffect, firePoint.position, Quaternion.identity);
            trail.gameObject.SetActive(true);
            Vector3 startPosition = trail.transform.position;
            StartCoroutine(SpownTrail(trail, endPosition, hitSomething, startPosition));
        }

        SendShootRpcs(hitSomething);
    }

    void SendShootRpcs(bool hitSomething)
    {
        LogWriter.WriteLog("SendShootRpcs");
        foreach(GameObject player in players)
        {
            shootingWithRaycasts currentPLayerShootingWithRaycasts = player.GetComponent<shootingWithRaycasts>();
            if (currentPLayerShootingWithRaycasts != null)
            {
                Vector3 endPosition = fpsCam.transform.position + (fpsCam.transform.forward * range);
                if (NetworkManager.Singleton.IsServer)
                {
                    currentPLayerShootingWithRaycasts.RenderBulletTracesClientRpc(endPosition, hitSomething);
                }
                else
                {
                    currentPLayerShootingWithRaycasts.RenderBulletTracesServerRpc(endPosition, hitSomething);
                }
            }
        }
    }

    //for simplicity im putting reycast rendering from otherplayers here:

    [ServerRpc(RequireOwnership = false)]
    public void RenderBulletTracesServerRpc(Vector3 endPosition, bool hitSomething)
    {
        LogWriter.WriteLog("RenderBulletTracesServerRpc");
        TrailRenderer trail = Instantiate(tracerEffect, firePoint.position, Quaternion.identity);
        trail.gameObject.SetActive(true);
        Vector3 startPosition = trail.transform.position;
        StartCoroutine(SpownTrail(trail, endPosition, hitSomething, startPosition));
    }

    [ClientRpc]
    public void RenderBulletTracesClientRpc(Vector3 endPosition, bool hitSomething)
    {
        LogWriter.WriteLog("RenderBulletTracesClientRpc");
        TrailRenderer trail = Instantiate(tracerEffect, firePoint.position, Quaternion.identity);
        trail.gameObject.SetActive(true);
        Vector3 startPosition = trail.transform.position;
        StartCoroutine(SpownTrail(trail, endPosition, hitSomething, startPosition));
    }

    IEnumerator SpownTrail(TrailRenderer trail, Vector3 endPosition, bool hitSomething, Vector3 startPosition)
    {
        LogWriter.WriteLog("SpownTrail");
        float time = 0f;
        while (time < 1f)
        {
            trail.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = endPosition;

        if (impactEffect != null && time >= 1f && hitSomething)
        {
            ParticleSystem newImpactEffect = Instantiate(impactEffect, endPosition, Quaternion.identity);
            Destroy(newImpactEffect, 1f);
        }

        Destroy(trail.gameObject, trail.time);
    }

}
