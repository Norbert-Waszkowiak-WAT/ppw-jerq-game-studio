using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingHandler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject weapon;
    public float bulletSpeed = 100f;
    public Camera playerCamera;
    public float timeToDestroy = 5f;

    private KeyCode shootingKey = KeyCode.Mouse0;
    private Transform bulletSpown;

    void Start()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).tag == "playerCamera")
            {
                playerCamera = transform.GetChild(i).GetComponent<Camera>();
                break;
            }
        }


        if (weapon != null) 
        {
            for (int i = 0; i < weapon.transform.childCount; i++)
            {
                if (weapon.transform.GetChild(i).gameObject.transform.name == "bulletSpown")
                {
                    bulletSpown = weapon.transform.GetChild(i).gameObject.transform;
                    break;
                }
            }
        } 
    }

    void Update()
    {
        if (bulletSpown != null && Input.GetKey(shootingKey) && playerCamera != null)
        {
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(playerCamera.transform.eulerAngles.x, bulletSpown.transform.eulerAngles.y, bulletSpown.transform.eulerAngles.z)); //shoot where camer is looking

            GameObject bullet = Instantiate(bulletPrefab, bulletSpown.transform.position, spawnRotation);

            Vector3 bulletVelocity = playerCamera.transform.forward * bulletSpeed; //add speed
            bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;

            bullet.tag = "bullet";

            Destroy(bullet, timeToDestroy);
        }
    }
}
