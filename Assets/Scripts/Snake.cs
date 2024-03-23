using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;

public class Snake : NetworkBehaviour
{
    [SyncVar] float speed = 3f;
    [SerializeField] float rotationSpeed = 180f, speedChange = 0.5f;
    [SerializeField] TailSpawner tailSpawner;
    [SerializeField] PlayerName playerName;
    public static event Action<PlayerName> OnPlayerServerSpawned;
    public static event Action<PlayerName> OnPlayerServerDespawned;

    public float Speed { get { return speed; } }

    public override void OnStartServer()
    {
        Food.OnEat += ChangePlayerSpeed;
        OnPlayerServerSpawned?.Invoke(playerName);
    }
    public override void OnStopServer()
    {
        Food.OnEat -= ChangePlayerSpeed;
    }
    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NetworkIdentity>(out NetworkIdentity identity) && identity.connectionToClient == connectionToClient) return;
        switch (other.tag)
        {
            case "Border":
            case "Player":
            case "Tail":
                DestroySelf();
                break;
        }
    }

    private void DestroySelf()
    {
        OnPlayerServerDespawned?.Invoke(playerName);
        foreach(var tail in tailSpawner.Tails)
        {
            NetworkServer.Destroy(tail);
        }
        NetworkServer.Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }

    void ChangePlayerSpeed(GameObject player)
    {
        if (player != gameObject) return;
        speed += speedChange;
    }
}
