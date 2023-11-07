# Lobby Beta

This is the Unity SDK for the Lobby Beta. See the [Lobby Dashboard](https://dashboard.unity3d.com/lobby) for more information.

## Installing Lobby

To install Lobby, use the Unity package manager.

1. Launch Unity
2. Window > Package Manager

## Unity Authentication Requirement

The Unity Lobby service requires using the Unity authentication package. To use it, install the `com.unity.services.authentication` package.

### Using Unity Authentication

To use authentication, you will need to import the package:

```csharp
using Unity.Services.Authentication;
```

Once imported, you will need to log in before using API calls.

Sample Usage:

```csharp
async void Start()
{
    // Anonymous Sign-In
    await UnityServices.Initialize();
    await AuthenticationService.Instance.SignInAnonymouslyAsync();

    if (AuthenticationService.Instance.IsSignedIn)
    {
        // Query for Lobbies
        await QuickJoinConquestLobby();
    }
    else
    {
        Debug.Log("Player was not signed in successfully?");
    }

}

async void QuickJoinConquestLobby()
{
    // Try to quickJoin a Conquest game
    QuickJoinRequest request = new QuickJoinRequest(){
        Filter = new List<QueryFilter>(){ 
            new QueryFilter(
                field: QueryFilter.FieldOptions.S1,
                op: QueryFilter.OpOptions.EQ,
                value: "Conquest")
        }
    };

    Response<Lobby> response = await LobbyService.LobbyApiClient.QuickJoinLobbyAsync(request);
    Debug.Log($"Joined lobby {response.Result.Id}");
}
```
