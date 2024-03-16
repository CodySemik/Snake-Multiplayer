using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Snake : NetworkBehaviour
{
    [SyncVar] float speed = 3f;
    [SerializeField] float rotationSpeed = 180f, speedChange = 0.5f;
    [SerializeField] GameObject tailPrefab;

    public float Speed { get { return speed; } }
    public List<GameObject> Tails { get; } = new List<GameObject>();

    public override void OnStartServer()
    {
        Tails.Add(gameObject);
        Food.OnEat += ChangePlayerSpeed;
    }
    public override void OnStopServer()
    {
        Food.OnEat -= ChangePlayerSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border")) SceneManager.LoadScene(0);
    }
    //void HandlePlayerSpeed(float oldSpeed, float newSpeed)
    //{
    //    print(newSpeed);
    //}

    void ChangePlayerSpeed(GameObject player)
    {
        if (player != gameObject) return;
        speed += speedChange;
    }
}
