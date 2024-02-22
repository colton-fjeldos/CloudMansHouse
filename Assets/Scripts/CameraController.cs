using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraOffset;
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y+cameraOffset, transform.position.z);
    }
}