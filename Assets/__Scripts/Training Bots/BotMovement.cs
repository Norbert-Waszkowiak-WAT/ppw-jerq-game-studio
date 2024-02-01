using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public List<Transform> positions;

    public Rigidbody rb;

    private List<Vector3> positionsVector3 = new List<Vector3>();

    public float speed = 1f;

    private int currentTarget = 0;

    private void Start()
    {
        foreach (Transform position in positions)
        {
            positionsVector3.Add(position.position);
        }
    }

    void Update()
    {
        //move using rigidbody
        transform.LookAt(positionsVector3[currentTarget]);
        Vector3 direction = positionsVector3[currentTarget] - transform.position;
        rb.velocity = direction.normalized * speed;
        if (Vector3.Distance(transform.position, positionsVector3[currentTarget]) < 5f)
        {
            currentTarget++;
            if (currentTarget >= positionsVector3.Count)
            {
                currentTarget = 0;
            }
        }
    }
}
