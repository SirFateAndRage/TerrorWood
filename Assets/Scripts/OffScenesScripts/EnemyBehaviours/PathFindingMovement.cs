using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class PathFindingMovement
{
    int currentPathindex = 0;
    DesiredVector _vector;
    float _maxSpeed;
    float _maxForce;
    Vector3 aux;
    PathFinding _pathFinding;
    Transform _enemy;
    Transform _target;

    public PathFindingMovement(DesiredVector d,float speed,float force,PathFinding p,Transform enemy,Transform target)
    {
        _vector = d;
        _maxSpeed = speed;
        _maxForce = force;
        _pathFinding = p;
        _enemy = enemy;
        _target = target;
    }

    public Vector3 MoveWithFinding(Vector3 _velocity)
    {
        if(_pathFinding.Path.Count > currentPathindex)
        {
            Debug.LogError("creo var");

                var targetPos = _pathFinding.Path[currentPathindex].Position;

            if(_vector.Desired(targetPos, _enemy.position).magnitude > 1f)
            {
                var goTo = _vector.Desired(targetPos, _enemy.position);
                goTo.Normalize();
                goTo *= _maxSpeed;
                aux = _vector.Steering(goTo, _velocity, _maxForce);
               
            }
            else
            {
                _pathFinding.Path.RemoveAt(0);
                if (_pathFinding.Path.Count <= 0)
                {
                    _pathFinding.Path.Clear();
                    currentPathindex = 0;
                }
                aux = Vector3.zero;
            }
        }

        return aux;
    }

    public void OnUpdate()
    {
        var dist = Vector3.Distance(_enemy.position, _target.position);
        if (dist > 4)// radio
        {
            if (_pathFinding.Path.Count <= 0 || Vector3.Distance(_target.transform.position, _pathFinding.Path[_pathFinding.Path.Count - 1].Position) > 4) //distancia mayor al ultimo nodo.
            {
                _pathFinding.FindPath(_enemy.position, _target.transform.position);
                Debug.LogError("arme Recorrido");
            }

        }

    }

}
