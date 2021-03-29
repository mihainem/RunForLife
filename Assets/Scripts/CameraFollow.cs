using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 temp = _transform.position;
        _transform.position = new Vector3(temp.x, temp.y, player.position.z - 10f);
    }
}
