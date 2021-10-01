using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement
{
    Vector3 _velocity;
    Transform _this;
    int _maxSpeed;
   
    public BaseMovement(Transform transform,int speed)
    {
        _this = transform;
        _maxSpeed = speed;
    }

    public Vector3 Velocity
    {
        get
        {
            return _velocity;
        }
    }
    public void OnUpdate()
    {
        _this.position += _velocity * Time.deltaTime;
        _this.forward = _velocity.normalized;

    }

    public void AddForce(Vector3 force)
    {
       
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

    }
}
