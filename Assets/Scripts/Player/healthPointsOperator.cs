using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPointsOperator : MonoBehaviour
{
    public float healthPoints = 100f;

    void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            healthPoints -= 1f;
            Destroy(collision.gameObject);
        }
    }
}
