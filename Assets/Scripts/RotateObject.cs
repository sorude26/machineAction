using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    float _rotateSpeed = -0.1f;
    private void LateUpdate()
    {
        transform.Rotate(Vector3.up, _rotateSpeed);
    }
}
