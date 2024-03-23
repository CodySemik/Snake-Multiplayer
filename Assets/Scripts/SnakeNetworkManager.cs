using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SnakeNetworkManager : NetworkManager
{
    [SerializeField] GameObject foodSpawnerPrefab, gameOverHandlerPrefab;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        if(numPlayers > 1)
        {
            GameObject foodSpawner = Instantiate(foodSpawnerPrefab);
            GameObject gameOverHandler = Instantiate(gameOverHandlerPrefab);
            NetworkServer.Spawn(foodSpawner);
            NetworkServer.Spawn(gameOverHandler);
        }
    }
}
// ДЕЛАТЬ В ОТДЕЛЬНОЙ ВЕТКЕ!!!
// В оригинальной игре змейка ускорялась
// каждый раз при поедании еды. Попробуй
// реализовать этот функционал, чтобы он
// работал в мультиплеерной версии.


