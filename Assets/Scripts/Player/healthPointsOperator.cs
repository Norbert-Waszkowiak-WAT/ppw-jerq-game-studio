using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPointsOperator : MonoBehaviour
{
    public float healthPoints = 100f;

    private Alteruna.Avatar avatar;

    void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();

        if (avatar != null && avatar.IsOwner)
            return;
    }

    void Update()
    {
        if (avatar != null && avatar.IsOwner)
            return;

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
