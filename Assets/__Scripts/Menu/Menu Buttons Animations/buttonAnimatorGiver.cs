using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace
using TMPro;

public class ButtonAnimatorGiver : MonoBehaviour
{
    public RuntimeAnimatorController controller; // Make sure to assign a controller in the Unity editor

    void Start()
    {
        Animator animator = gameObject.AddComponent<Animator>(); // Use gameObject.AddComponent<Animator>() to add the Animator component
        animator.runtimeAnimatorController = controller; // Use runtimeAnimatorController to set the controller

        Button button = GetComponent<Button>(); // Use UnityEngine.UI.Button for UI buttons
        button.transition = Selectable.Transition.Animation; // Use uppercase Transition

        // If you are using TextMeshPro's TMP_Button, replace the above two lines with the following:
        //TMP_Button button = GetComponent<TMP_Button>();
        //button.transition = Selectable.Transition.Animation;
    }
}
