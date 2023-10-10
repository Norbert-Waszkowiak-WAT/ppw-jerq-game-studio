
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(shootButton) && !reloading)
        {
            if (weaponsCanShoot[gun.GetComponent<GunStats>().thisWeapon.weaponIndex])
            {
                if (!(weaponsMagazines[gun.GetComponent<GunStats>().thisWeapon.weaponIndex] <= 0))
                {
                    Shoot();
                    weaponsMagazines[gun.GetComponent<GunStats>().thisWeapon.weaponIndex]--;
                    weaponsCanShoot[gun.GetComponent<GunStats>().thisWeapon.weaponIndex] = false;
                    StartCoroutine(ShootDelay());
                }
            }
        }

        if (Input.GetKeyDown(reloadButton) && !reloading)
        {
            StartCoroutine(Reloading());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.transform.GetComponent<Target>().TakeDamageServerRpc(damage);
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
    }

    public void WaponChanged()
    {
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
        gun.GetComponent<Fire>().fired = true;
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Create a raycast
        RaycastHit hit;
        Vector3 endPosition;

        bool hitSomething = false;

        int layerMask = ~LayerMask.GetMask("Ignore Raycast");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask, QueryTriggerInteraction.Collide))
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
                }
                else if (hittedHitBox.hitboxType == "leg" || hittedHitBox.hitboxType == "arm")
                {
                    damageMultiplier = legshotMultiplier;
                    Debug.LogError("legshot");
                }
                else
                {
                    Debug.LogError("bodyshot");
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
            StartCoroutine(SpownTrail(trail, endPosition, hitSomething));
        }
    }

    IEnumerator SpownTrail(TrailRenderer trail, Vector3 endPosition, bool hitSomething)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;
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
