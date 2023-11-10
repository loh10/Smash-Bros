using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MultiplayerManager : MonoBehaviour
{
    public static int playerIndex;
    public Transform[] spawnPoints;

    private void Awake()
    {
        playerIndex = 0;
    }

    public void setSpawns(PlayerInput playerInput)
    {
        playerIndex++;
        Debug.Log("Player joined. Player index: " + playerIndex);

        int spawnIndex = (playerIndex - 0) % spawnPoints.Length;
        Transform spawnPoint = spawnPoints[spawnIndex];

        Debug.Log("Spawn Index: " + spawnIndex);
        Debug.Log("Spawn Point: " + spawnPoint.position);

        playerInput.transform.position = spawnPoint.position;
    }
}
