using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    Transform target;
    [SerializeField]
     float smoothSpeed = 15f;
    [SerializeField]
    Vector3 offset;

    private void FixedUpdate()
    {
        debug.log("hola");
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
