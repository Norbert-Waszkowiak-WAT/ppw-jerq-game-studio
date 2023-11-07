using System.IO;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using System.Threading.Tasks;

public class testRelay : MonoBehaviour
{

    public GameObject joinButtons;
    public GameObject minimap;

    private async void Start()
    {
        minimap.SetActive(false);
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        else
        {
            Debug.Log("Already signed in " + AuthenticationService.Instance.PlayerId);
        }
        
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateRelay();
        }
    }

    */

    public async void CreateRelay()
    {
        try 
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        
            Debug.Log("Join code: " + joinCode);

            SaveToFile(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);


            NetworkManager.Singleton.StartHost();

            joinButtons.SetActive(false);
            minimap.SetActive(true);
        } catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
        
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining relay with code: " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            //player isnt spowning

            NetworkManager.Singleton.StartClient();
            
            joinButtons.SetActive(false);
            minimap.SetActive(true);
        } catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    void SaveToFile(string toSave)
    {
        // Specify the file name (change as needed)
        string fileName = "code.txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            // Write the string to the file
            File.WriteAllText(filePath, toSave);
            Debug.LogError("File saved successfully to: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }

    public async Task<string> CreateRelayForLobby()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log("Join code: " + joinCode);

            SaveToFile(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);


            NetworkManager.Singleton.StartHost();

            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
            return null;
        }

    }
}
