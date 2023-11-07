using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inLobbyPlayerPreviews : MonoBehaviour
{
    public GameObject cameraPrefab;
    public GameObject playerPrefab;

    public RawImage textureOutput;

    private void Start()
    {
        GameObject camera = Instantiate(cameraPrefab, Vector3.zero, Quaternion.identity);
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        RenderTexture renderTexture = new RenderTexture(307, 550, 16, RenderTextureFormat.ARGB32);

        player.transform.position = new Vector3(1000, 100, -142);
        camera.transform.position = new Vector3(1059, 192, -1002);

        camera.GetComponent<Camera>().targetTexture = renderTexture;
        textureOutput.texture = renderTexture;
    }
}
