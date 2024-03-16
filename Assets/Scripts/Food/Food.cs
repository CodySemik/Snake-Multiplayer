using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : NetworkBehaviour
{
    [SerializeField] GameObject particlePrefab;
    GameObject boom = null;
    public static event Action<GameObject> OnEat;
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ServerParticles();
        OnEat?.Invoke(other.gameObject);
        NetworkServer.Destroy(gameObject);
    }
    IEnumerator DestroyBoom(float time)
    {
        yield return new WaitForSeconds(time);
        NetworkServer.Destroy(boom);
    }
    [ServerCallback]
    void ServerParticles()
    {
        boom = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
        NetworkServer.Spawn(boom);
        StartCoroutine(DestroyBoom(1f));
    }
}
