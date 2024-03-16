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
        GameObject newTail =  Instantiate(tailPrefab, Tails.Count == 0? transform.position : Tails[Tails.Count - 1].transform.position, Quaternion.identity);
        NetworkServer.Spawn(newTail, connectionToClient);
        Tails.Add(newTail);
        //speed += speedChange;
    }
}
