using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public float fireRate;
    public float reloadSpeed;
    public int magazineSize;
    public float range;
    public int bulletsPerShot;
    
    public float headshotMultiplier;
    public float legshotMultiplier;

    public int weaponIndex;

    public Vector3 muzzlePosition;
    public Vector3 weaponPosition;
    public Quaternion weaponRotation;
}
