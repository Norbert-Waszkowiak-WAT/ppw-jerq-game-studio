using UnityEngine;
using TMPro;

public class TMPInputFieldReader : MonoBehaviour
{
    public TMP_InputField tmpInputField;
    public testRelay testRelayInstance;

    private void Start()
    {
        tmpInputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string inputValue = tmpInputField.text;
            Debug.Log("Input Value: " + inputValue);
            testRelayInstance.JoinRelay(inputValue);
        }
    }
}
