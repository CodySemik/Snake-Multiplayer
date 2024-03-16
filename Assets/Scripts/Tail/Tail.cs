using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Tail : NetworkBehaviour
{
    [SyncVar] Snake owner;
    [SyncVar] GameObject target;

    public GameObject Target { get => target; private set => target = value; }
    public Snake Owner { get => owner; private set => owner = value; }

    public override void OnStartServer()
    {
        owner = connectionToClient.identity.GetComponent<Snake>(); // identity это грубо говоря текущий префаб игрока
        var tails = owner.GetComponent<TailSpawner>().Tails;
        target = tails.Count == 0? owner.gameObject : tails[tails.Count - 1];
    }
}
