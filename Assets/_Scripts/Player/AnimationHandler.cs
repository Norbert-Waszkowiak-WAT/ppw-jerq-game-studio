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
    private List<string> animationNamesJumping = new List<string>();

    public string lastAnimationName = "T-Pose";
    public string newAnimationName = "T-Pose";

    public float jumpUpDownBarier;

    private void Start()
    {
        //1 crouching, 2 standing
        animationNamesCrouchSpeed.Add("IdleCrouching");//
        animationNamesCrouchSpeed.Add("Idle");//
        
        animationNamesWalkSpeed.Add("CrouchWalk");//
        animationNamesWalkSpeed.Add("Walking");//
        
        animationNamesRunSpeed.Add("CrouchRun");//
        animationNamesRunSpeed.Add("RunForward");//

        animationNamesJumping.Add("Jump Up");
        animationNamesJumping.Add("Jump Loop");
        animationNamesJumping.Add("Jump Down");
    }

    private void Update()
    { 
        if (playerMovementInsance != null && animator != null)
        {
            speed = playerMovementInsance.currentSpeed;
            int crouching = 1;
            if (playerMovementInsance.isCrouching) crouching = 0;
            float distanceToGround = playerMovementInsance.distanceToGround;
            bool grounded = playerMovementInsance.grounded;
            newAnimationName = getNewAnimation(grounded, distanceToGround, crouching);
            if(newAnimationName != lastAnimationName)
            {
                Debug.Log("Changing animation from " + lastAnimationName + " to " + newAnimationName);
                lastAnimationName = newAnimationName;
                animator.SetTrigger(newAnimationName);
            }
        }
    }

    private string getNewAnimation(bool grounded, float distanceToGround, int crouching)
    {
        if (!grounded)
        {
            if (distanceToGround <= jumpUpDownBarier)
            {
              /**  if (lastAnimationName == animationNamesJumping[1])
                {
                    return animationNamesJumping[2];
                } */
                return animationNamesJumping[0];
            }
            else
            {
                return animationNamesJumping[1];
            }
        }
        else
        {
            if (speed < walkSpeedTreshold)
            {
                return animationNamesCrouchSpeed[crouching];
            }
            else if (speed < runSpeedTreshold)
            {
                return animationNamesWalkSpeed[crouching];
            }
            else if (speed >= runSpeedTreshold)
            {
                return animationNamesRunSpeed[crouching];
            }
            else
            {
                return "T-Pose";
            }
        }
    }
}
