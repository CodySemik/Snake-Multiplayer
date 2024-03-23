using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : NetworkBehaviour
{
    [SerializeField] TMPro.TMP_Text playerNameText;
    [SyncVar(hook = nameof(HandlePlayerNameUpdated))] string playerName;

    public string PlayerNameProp { get => playerName; private set => playerName = value; }

    public override void OnStartServer()
    {
        PlayerNameProp = $"player {connectionToClient.connectionId}";
    }
    public void HandlePlayerNameUpdated(string oldName, string newName)
    {
        playerNameText.text = newName;
    }
}
