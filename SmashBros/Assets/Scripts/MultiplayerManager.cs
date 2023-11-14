using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MultiplayerManager : MonoBehaviour
{
    public int playerIndex;
    private bool[] spawnPointsOccupied;
    public Transform[] spawnPoints;
    public int Nbplayer;

    private void Awake()
    {
        playerIndex = 0;
        spawnPointsOccupied = new bool[spawnPoints.Length];
    }

    public void setSpawns(PlayerInput playerInput)
    {
        
        playerIndex++;
        Nbplayer = playerIndex;
        //Debug.Log("Player joined. Player index: " + playerIndex);

        int spawnIndex = GetNextAvailableSpawnIndex();
        if (spawnIndex != -1)
        {
            Transform spawnPoint = spawnPoints[spawnIndex];
            spawnPointsOccupied[spawnIndex] = true;

            playerInput.transform.position = spawnPoint.position;
            //Debug.Log("Player joined. Player index: " + playerIndex + ". Spawn index: " + spawnIndex);
        }
    }

    private int GetNextAvailableSpawnIndex()
    {
        for (int i = 0; i < spawnPointsOccupied.Length; i++)
        {
            if (!spawnPointsOccupied[i])
            {
                return i;
            }
        }
        
        return -1;
    }
}

