using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    public MonoBehaviour scriptToInteract;
    public string methodToCall;

    public void Interact()
    {
        scriptToInteract.Invoke(methodToCall, 0f);
    }
}
