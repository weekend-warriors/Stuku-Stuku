using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GameStateController : NetworkBehaviour
{
    public int ConnectedStartCount;
    public GameObject Title;

    [SyncVar(hook = nameof(CheckIfStarted))]
    private int ConnectedPlayers;
    private bool IsStarted = false;

    private Guid LocalPlayerId;

    void LateUpdate()
    {
        if (isServer && !IsStarted)
        {
            var readyPlayers = 0;

            foreach (var connection in NetworkServer.connections)
            {
                if (connection.Value.isReady)
                {
                    readyPlayers++;
                }
            }

            ConnectedPlayers = readyPlayers;

            if (ConnectedPlayers == ConnectedStartCount)
            {
                IsStarted = true;
                var vadaIndex = UnityEngine.Random.Range(0, ConnectedPlayers);
                var players = GameObject.FindGameObjectsWithTag("Player");
                var runnerIndex = 0;

                for (int i = 0; i < players.Length; i++)
                {
                    var controller = players[i].GetComponent<PlayerController>();

                    if (i == vadaIndex)
                    {
                        controller.BecomeVada();
                    }
                    else
                    {
                        controller.BecomeRunner(runnerIndex);
                        runnerIndex++;
                    }
                } 
            }
        }
    }

    private void CheckIfStarted(int old, int value)
    {
        if (value == ConnectedStartCount)
        {
            Title.SetActive(false);
        }
    }
}
