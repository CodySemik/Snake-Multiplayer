using Mirror;
using System.Collections;
using System.Collections.Generic;
using Telepathy;
using UnityEngine;

public class FoodSpawner : NetworkBehaviour
{
    [SerializeField] GameObject foodPrefab;
    [SerializeField] float xSize = 8f, zSize = 8f;

    public override void OnStartServer()
    {
        SpawnFood(gameObject);
        Food.OnEat += SpawnFood;
    }
    public override void OnStopServer()
    {
        Food.OnEat -= SpawnFood;
    }
    [Server] // Этот атрибут говорит чтобы команда запускалась ТОЛЬКО на сервере
    public void SpawnFood(GameObject player)
    {
        Vector3 pos = new Vector3(
            Random.Range(-xSize, xSize),
            foodPrefab.transform.position.y,
            Random.Range(-zSize, zSize));
        GameObject newFood = Instantiate(foodPrefab, pos, foodPrefab.transform.rotation);
        NetworkServer.Spawn(newFood);
    }
}
