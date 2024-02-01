using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandlerSingleplayer : MonoBehaviour
{
    public bool singleAnimation = false;
    public string singleAnimationName = "T-Pose";
    public bool debugAnimationChanges = false;
    public PlayerMovementSingleplayer playerMovementInsance;

    public bool canJump = true;

    public Animator animator;

    public float walkSpeedTreshold;
    public float runSpeedTreshold;

    private float speed;
    private bool isSprinting;

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
        if (singleAnimation)
        {
            if (lastAnimationName != singleAnimationName || singleAnimationName != animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
            {
                Debug.Log(singleAnimationName);
                animator.SetTrigger(singleAnimationName);
                lastAnimationName = singleAnimationName;
            }
        }
        if (playerMovementInsance != null && animator != null)
        {
            speed = playerMovementInsance.currentSpeed;
            isSprinting = playerMovementInsance.isSprinting;
            int crouching = 1;
            if (playerMovementInsance.isCrouching) crouching = 0;
            float distanceToGround = playerMovementInsance.distanceToGround;
            bool grounded = playerMovementInsance.grounded;
            newAnimationName = getNewAnimation(grounded, distanceToGround, crouching, isSprinting);
            string playingAnimationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (newAnimationName != lastAnimationName || newAnimationName != playingAnimationName)
            {
                if (newAnimationName != playingAnimationName)
                {
                    if (debugAnimationChanges) Debug.Log("playing animation: " + playingAnimationName + " instead of " + newAnimationName);
                    LogWriter.WriteLog("playing animation: " + playingAnimationName + " instead of " + newAnimationName);
                }
                if (debugAnimationChanges) Debug.Log("Changing animation from " + lastAnimationName + " to " + newAnimationName);
                LogWriter.WriteLog("Changing animation from " + lastAnimationName + " to " + newAnimationName);
                lastAnimationName = newAnimationName;
                animator.SetTrigger(newAnimationName);
            }
        }
    }

    private string getNewAnimation(bool grounded, float distanceToGround, int crouching, bool isSprinting)
    {
        if (!grounded && distanceToGround > 0.15f && canJump)
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
            else if (!isSprinting)
            {
                return animationNamesWalkSpeed[crouching];
            }
            else if (isSprinting)
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
