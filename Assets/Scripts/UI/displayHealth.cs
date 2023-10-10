using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class displayHealth : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        //get all gameobjects with the tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            //if the player is not the local player
            if (player.GetComponent<NetworkObject>().IsLocalPlayer)
            {
                //get the healthbar component
                Target playerTarget = player.GetComponent<Target>();
                //set the max health
                
                playerTarget.healthBar = transform.GetComponent<healthBar>();
                playerTarget.Awake();
            }
        }
    }

}
