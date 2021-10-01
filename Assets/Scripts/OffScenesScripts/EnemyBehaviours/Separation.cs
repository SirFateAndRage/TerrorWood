using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class Separation : MonoBehaviour
{
   
    DesiredVector _vector;
    Transform _enemy;
    float _separationDistance;
    float _maxSpeed;
    float _maxForce;

    public Separation(DesiredVector d, Transform en, float distSeparation,float speed, float force)
    {
        _vector = d;
        _enemy = en;
        _separationDistance = distSeparation;
        _maxForce = force;
        _maxSpeed = speed;
       
    }

    public  Vector3 SepationEn(Vector3 _velocity)
    {
        int neightboards = 0;
        Vector3 desired = Vector3.zero;
        if (DataManager.instance.allenviromentData == null) return Vector3.zero;
        foreach (var data in DataManager.instance.allenviromentData)
        {
            Vector3 distance = _vector.Desired(data.position, _enemy.position);
            if(data != _enemy && distance.magnitude < _separationDistance)
            {
                desired.x = distance.x;
                desired.z = distance.z;
                neightboards++;

            }
        }
        if (neightboards == 0) return desired;

        desired /= neightboards;

        //
        desired *= -1;

        desired.Normalize();
        desired *= _maxSpeed;

        Vector3 datareturn = _vector.Steering(desired, _velocity, _maxForce);

        return datareturn;


    }
}
