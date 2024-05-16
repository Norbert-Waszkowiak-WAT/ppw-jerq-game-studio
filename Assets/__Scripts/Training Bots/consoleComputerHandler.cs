using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class consoleComputerHandler : MonoBehaviour
{
    public GameObject player;

    public saveLoadShootingRange saveLoadShootingRange;

    public Outline outline;

    public GameObject consoleComputerImage;

    public bool isPlayerNear = false;

    public GameObject cameraHolder;
    public GameObject playerCamera;

    public Vector3 cameraPosition = new Vector3(0, 0.55f, 0);

    public Transform cameraDestination;

    public KeyCode consoleComputerKey = KeyCode.F;

    public float timeToMoveCamera = 3f;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2.5)
        {
            isPlayerNear = true;
            outline.OutlineWidth = 20;
            consoleComputerImage.active = true;
        }
        else
        {
            isPlayerNear = false;
            outline.OutlineWidth = 0;
            consoleComputerImage.active = false;
        }
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(consoleComputerKey))
            {
                DoTheTrick();
            }
        }
    }

    private void DoTheTrick()
    {
        player.GetComponent<PlayerMovementSingleplayer>().enabled = false;
        player.GetComponent<shootingWithRaycastsSingleplayer>().enabled = false;
        saveLoadShootingRange.SaveShootingRange();
        StartCoroutine(MoveCamera());
    }

    private void Awake()
    {
        saveLoadShootingRange.LoadShootingRange();
    }

    IEnumerator MoveCamera()
    {
        float elapsedTime = 0;
        Vector3 startingPos = playerCamera.transform.position;
        Quaternion startingRot = playerCamera.transform.rotation;
        playerCamera.transform.SetParent(null);
        while (elapsedTime < timeToMoveCamera)
        {
            playerCamera.transform.position = Vector3.Lerp(startingPos, cameraDestination.position, elapsedTime/timeToMoveCamera);
            playerCamera.transform.rotation = Quaternion.Lerp(startingRot, cameraDestination.rotation, elapsedTime/timeToMoveCamera);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.position = cameraDestination.position;
        playerCamera.transform.rotation = cameraDestination.rotation;
        SceneManager.LoadScene("ConsoleComputerScene");
    }
}
