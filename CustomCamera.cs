using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    Transform player = null;
    Vector3 playerOldpos = Vector3.zero;
    public GameObject _player;

    const float zdelta = -6.2f;
    const float ydelta = 9.1f;
    const float xdelta = 2f;
    const float xangle = 48.6f;
    const float yangle = -20.2f;

    public Transform PLAYER
    {
        get { return player; }
        set { player = value; }
    }

    void Start()
    {
        PLAYER = _player.transform;
        playerOldpos = player.position;
        Vector3 campos = player.position;
        campos.z += zdelta;
        campos.y += ydelta;
        campos.x += xdelta;
        transform.position = campos;

        Vector3 angle = Vector3.zero;
        angle.x = xangle;
        angle.y = yangle;
        transform.localEulerAngles = angle;
        
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 delta = player.position - playerOldpos;
        transform.position += delta;
        playerOldpos = player.position;
    }
}
