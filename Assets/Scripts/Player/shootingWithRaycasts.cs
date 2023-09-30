
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingWithRaycasts : MonoBehaviour
{
    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    public ParticleSystem impactEffect;

    public TrailRenderer tracerEffect;
    public float damage = 10f;
    public float range = float.MaxValue;

    public Camera fpsCam;

    public float fireRate = 15f;

    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (canShoot)
            {
                Shoot();
                canShoot = false;
                StartCoroutine(ShootDelay());
            }
        } 
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1f / fireRate);
        canShoot = true;
    }

    void Shoot ()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
        // Creating a raycast
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Debugging the raycast
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            TrailRenderer trail = Instantiate(tracerEffect, firePoint.position, Quaternion.identity);
            StartCoroutine(SpownTrail(trail, hit.point));
        }

        IEnumerator SpownTrail(TrailRenderer trail, Vector3 hitPoint)
        {
            float time = 0f;
            Vector3 startPosition = trail.transform.position;
            while (time < 1f)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hitPoint, time);
                time += Time.deltaTime / trail.time;
                yield return null;
            }
            trail.transform.position = hitPoint;
            ParticleSystem newImpactEffect = Instantiate(impactEffect, hitPoint, Quaternion.LookRotation(hit.normal));

            Destroy(trail.gameObject, trail.time);
            Destroy(newImpactEffect, 1f);
        }
    }
}
