using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class weaponSlot : MonoBehaviour, IDropHandler
{
    public Image backgroundImage;
    public bool isDesiredSpot = true;
    public string weaponName;
    public savingAndLoadnigWeaponsLoadout savingAndLoadnigWeaponsLoadout;
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        
        GameObject weapon = eventData.pointerDrag.gameObject;
        LogWriter.WriteLog(gameObject.name + " OnDrop " + weapon.name);
        if (transform.childCount == 0)
        {
            if ((isDesiredSpot) || (!isDesiredSpot && weapon.name == weaponName))
            {
                weapon.transform.SetParent(transform);
                weapon.transform.position = transform.position;
                weapon.transform.rotation = transform.rotation;
            }
        }
        else
        {
            if ((isDesiredSpot) || (!isDesiredSpot && weapon.name == weaponName))
            {
                //get all children of the this transform
                foreach (Transform child in transform)
                {
                    if (child.gameObject.GetComponent<moveableWeapon>() != null)
                    {
                        child.transform.parent = child.gameObject.GetComponent<moveableWeapon>().weaponTemplate.transform;
                        child.transform.position = child.gameObject.GetComponent<moveableWeapon>().weaponTemplate.transform.position;
                        child.transform.rotation = child.gameObject.GetComponent<moveableWeapon>().weaponTemplate.transform.rotation;
                    }   
                }
                weapon.transform.SetParent(transform);
                weapon.transform.position = transform.position;
                weapon.transform.rotation = transform.rotation;
            }
        }
        savingAndLoadnigWeaponsLoadout.SaveWeaponLoadout();

        
    }
}
