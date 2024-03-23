using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GameOverHandler : NetworkBehaviour
{
    List<PlayerName> playerNames = new List<PlayerName>();
    public override void OnStartServer()
    {
        Snake.OnPlayerServerSpawned += HandlePlayerSpawn;
        Snake.OnPlayerServerDespawned += HandlePlayerDespawn;
    }
    public override void OnStopServer()
    {
        Snake.OnPlayerServerSpawned -= HandlePlayerSpawn;
        Snake.OnPlayerServerDespawned -= HandlePlayerDespawn;
    }
    private void HandlePlayerSpawn(PlayerName playerName)
    {
        playerNames.Add(playerName);
    }
    private void HandlePlayerDespawn(PlayerName playerName)
    {
        playerNames.Remove(playerName);
        if (playerNames.Count == 1 )
        {
            print($"Player {playerNames[0].PlayerNameProp} won!");
        }
    }
}
