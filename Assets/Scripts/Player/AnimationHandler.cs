using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public PlayerMovement playerMovementInsance;

    public Animator animator;

    public float walkSpeedTreshold;
    public float runSpeedTreshold;

    private float speed;

    private List<string> animationNamesCrouchSpeed = new List<string>();
    private List<string> animationNamesWalkSpeed = new List<string>();
    private List<string> animationNamesRunSpeed = new List<string>();

    private void Start()
    {
        //1 crouching, 2 standing
        animationNamesCrouchSpeed.Add("IdleCrouching");//
        animationNamesCrouchSpeed.Add("Idle");//
        
        animationNamesWalkSpeed.Add("CrouchWalk");//
        animationNamesWalkSpeed.Add("Walking");//
        
        animationNamesRunSpeed.Add("CrouchRun");//
        animationNamesRunSpeed.Add("RunForward");//
    }

    private void Update()
    {
        if (playerMovementInsance != null && animator != null)
        {
            speed = playerMovementInsance.currentSpeed;
            int crouching = 1;
            if (playerMovementInsance.isCrouching) crouching = 0;
            if (speed < walkSpeedTreshold)
            {
                animator.Play(animationNamesCrouchSpeed[crouching]);
            } else if (speed < runSpeedTreshold)
            {
                animator.Play(animationNamesWalkSpeed[crouching]);
            } else if (speed >= runSpeedTreshold)
            {
                animator.Play(animationNamesRunSpeed[crouching]);
            } else
            {
                animator.Play("T-Pose");
            }
        }
    }
}
