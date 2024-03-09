using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField] GameObject camera;
    public override void OnStartAuthority() // ��� ���������� ������ � �������
    {
        camera.SetActive(true);
    }
}
