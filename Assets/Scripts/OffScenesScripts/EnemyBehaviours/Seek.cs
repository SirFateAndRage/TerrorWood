using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class Seek
{
    Transform _enemy;
    Transform _target;
    DesiredVector _desiredVector;
    float _maxForce;


    public Seek(Transform enemy,Transform target,float force,DesiredVector d)
    {
        _enemy = enemy;
        _target = target;
        _maxForce = force;
        _desiredVector = d;
    }

  

    public Vector3 SeekToTarget(Vector3 _velocity,float _maxSpeed)
    {
        if (_maxSpeed == 0) return Vector3.zero;
        var seekTo = _desiredVector.Desired(_target.position, _enemy.position);
        seekTo.Normalize();
        seekTo *= _maxSpeed;
        return _desiredVector.Steering(seekTo, _velocity, _maxForce);

    }

}
