using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using Unity.VisualScripting;

public class TailMovement : NetworkBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Tail tail;
    [SyncVar] Snake owner;
    [SyncVar] GameObject target;

    public Snake Owner { get => owner; private set => owner = value; }
    public GameObject Target { get => target; private set => target = value; }

    public override void OnStartServer()
    {
        owner = connectionToClient.identity.GetComponent<Snake>();
        var tails = owner.GetComponent<TailSpawner>().Tails;
        target = tails.Count == 0 ? owner.gameObject : tails[tails.Count - 1];
        tails.Add(gameObject);
        transform.position = target.transform.position;
    }
    void Update()
    {
        agent.speed = owner.Speed;
        agent.SetDestination(target.transform.position);
    }
}
