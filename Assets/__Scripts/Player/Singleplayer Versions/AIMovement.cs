using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Vector3 moveToPosition;
    public bool move = false;

    public NavMeshAgent agent;
    void Update()
    {
        if (moveToPosition != null && move)
        {
            if (move)
            {
                agent.SetDestination(moveToPosition);
                move = false;
            } else
            {
                agent.SetDestination(transform.position);
            }
        }
    }
}
