using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class moveableWeapon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public GameObject desiredSpot;
    private Quaternion baseRotation;
    private Vector3 basePosition;
    public GameObject weaponList;
    public GameObject tMPWeaponHolder;
    public GameObject weaponTemplate;

    public GameObject background;

    private GameObject previousParent;

    void Start()
    {
        basePosition = transform.position;
        baseRotation = transform.rotation;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        LogWriter.WriteLog("Weapon " + transform.name + " dragged");
        transform.GetComponent<Image>().raycastTarget = false;
        previousParent = transform.parent.gameObject;
        transform.SetParent(tMPWeaponHolder.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        transform.rotation = GetRotation();
    }

    private Quaternion GetRotation()
    {
        float maxDistance = Vector3.Distance(basePosition, desiredSpot.transform.position) - 100f;
        float currentDistance = Vector3.Distance(transform.position, desiredSpot.transform.position);
        float rotationValue = currentDistance/maxDistance;
        if (rotationValue > 1)
        {
            desiredSpot.GetComponent<Image>().color = new Color(desiredSpot.GetComponent<Image>().color.r, desiredSpot.GetComponent<Image>().color.g, desiredSpot.GetComponent<Image>().color.b, 1f);
            return baseRotation;
        }
        Vector3 eulerRotation = new Vector3(
            Mathf.Lerp(desiredSpot.transform.rotation.eulerAngles.x, baseRotation.eulerAngles.x, rotationValue),
            Mathf.Lerp(desiredSpot.transform.rotation.eulerAngles.y, baseRotation.eulerAngles.y, rotationValue),
            Mathf.Lerp(desiredSpot.transform.rotation.eulerAngles.z, baseRotation.eulerAngles.z, rotationValue)
            );

        desiredSpot.GetComponent<Image>().color = new Color(desiredSpot.GetComponent<Image>().color.r, desiredSpot.GetComponent<Image>().color.g, desiredSpot.GetComponent<Image>().color.b, rotationValue + 0.5f);

        return Quaternion.Euler(eulerRotation);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LogWriter.WriteLog("Weapon " + transform.name + " dropped");
        desiredSpot.GetComponent<Image>().color = new Color(desiredSpot.GetComponent<Image>().color.r, desiredSpot.GetComponent<Image>().color.g, desiredSpot.GetComponent<Image>().color.b, 0f);


        if (transform.parent.gameObject == desiredSpot)
        {
            desiredSpot.GetComponent<Image>().color = new Color(desiredSpot.GetComponent<Image>().color.r, desiredSpot.GetComponent<Image>().color.g, desiredSpot.GetComponent<Image>().color.b, 0f);
            transform.GetComponent<Image>().raycastTarget = true;
            return;
        }

        desiredSpot.GetComponent<Image>().color = new Color(desiredSpot.GetComponent<Image>().color.r, desiredSpot.GetComponent<Image>().color.g, desiredSpot.GetComponent<Image>().color.b, 1f);

        transform.parent = weaponTemplate.transform;
        
        transform.rotation = baseRotation;

        transform.position = new Vector3(background.transform.position.x, background.transform.position.y, basePosition.z);

        transform.GetComponent<Image>().raycastTarget = true;
    }
}
