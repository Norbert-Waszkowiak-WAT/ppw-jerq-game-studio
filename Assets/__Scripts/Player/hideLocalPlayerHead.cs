using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideLocalPlayerHead : MonoBehaviour
{
    public List<GameObject> headComponents = new List<GameObject>();
    private void Start()
    {
        foreach (GameObject headComponent in headComponents)
        {
            headComponent.layer = 9;
        }
    }
}
