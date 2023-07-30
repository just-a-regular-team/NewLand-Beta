using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0,0,-10f);
    public float dumping = 0.2f;

    private Vector3 velocity = Vector3.zero;

    public void FixedUpdate()
    {
        Vector3 movePos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position,movePos,ref velocity,dumping);
    }
}
