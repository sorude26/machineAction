using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    Transform _followTarget = default;
    [SerializeField]
    Transform _rotationTarget = default;
    [SerializeField]
    float _followSpeed = 1f;
    [SerializeField]
    float _rotationSpeed = 1f;
    [SerializeField]
    float _followMaxSpeed = 5f;
    [SerializeField]
    float _rotationMaxSpeed = 5f;
    private Vector3 _currentVelocityAngle;
    private Vector3 _currentVelocityPosition;
    //private void FixedUpdate()
    //{
    //    if (_followTarget == null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    transform.forward = Vector3.SmoothDamp(transform.forward, _rotationTarget.forward, ref _currentVelocityAngle, _rotationSpeed);
    //    float speed = (transform.position - _followTarget.position).sqrMagnitude;
    //    if (speed <= 0)
    //    {
    //        speed = 1;
    //    }
    //    transform.position = Vector3.SmoothDamp(transform.position, _followTarget.position, ref _currentVelocityPosition, _followSpeed / speed);
    //}
    private void FixedUpdate()
    {
        if (_followTarget == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.forward = Vector3.Lerp(transform.forward, _rotationTarget.forward, _rotationSpeed * Time.deltaTime);
        float speed = (transform.position - _followTarget.position).sqrMagnitude;
        transform.position = Vector3.Lerp(transform.position, _followTarget.position, speed * _followSpeed * Time.deltaTime);
    }
}
