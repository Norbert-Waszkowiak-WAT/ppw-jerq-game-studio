using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPlayerAnimation : MonoBehaviour
{
    public Animator menuPlayerAnimator;
    private int currentAnimationIndex = 0;

    public AnimationClip[] animationNames;

    /*private void Update()
    {
        menuPlayerAnimator.Play(animationNames[14].name);
    }*/
    public void NextAnimation()
    {
        currentAnimationIndex++;
        if (currentAnimationIndex >= animationNames.Length)
        {
            currentAnimationIndex = 0;
        }
        menuPlayerAnimator.Play(animationNames[currentAnimationIndex].name);
    }
}
