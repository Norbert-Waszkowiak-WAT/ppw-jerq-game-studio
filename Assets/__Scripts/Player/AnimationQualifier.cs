using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQualifier : MonoBehaviour
{
    public float speedTreshold;
    public GameObject player;

    public bool walkingAllowed;
    public bool tPoseAllowed;

    private Animator playerAnimator;
    private PlayerControl playerControl;

    private bool walkingSignalSended = false;
    private bool tPoseSignalSended = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerControl = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator != null && playerControl != null) 
        {
            if (playerControl.speed > speedTreshold && !walkingSignalSended && walkingAllowed) 
            {
                playerAnimator.SetTrigger("Walk");
                walkingSignalSended = true;
                tPoseSignalSended = false;
            }
            else if(playerControl.speed < speedTreshold && !tPoseSignalSended && tPoseAllowed)
            {
                playerAnimator.SetTrigger("T-Pose");
                walkingSignalSended = false;
                tPoseSignalSended = true;
            }
        }
    }
}
