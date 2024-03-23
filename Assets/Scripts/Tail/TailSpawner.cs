using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TailSpawner : NetworkBehaviour
{
    [SerializeField] GameObject tailPrefab;
    public List<GameObject> Tails {get;} = new List<GameObject>();
    public override void OnStartServer()
    {
        Food.OnEat += AddTail;
    }
    public override void OnStopServer()
    {
        Food.OnEat -= AddTail;
    }
    void AddTail(GameObject player)
    {
        if (player != gameObject) return;
        GameObject newTail =  Instantiate(tailPrefab);
        NetworkServer.Spawn(newTail, connectionToClient);
        //speed += speedChange;
    }
}
