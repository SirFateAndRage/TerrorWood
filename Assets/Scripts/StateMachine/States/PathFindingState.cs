using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class PathFindingState : IState
{
    int currentPathindex = 0;
    DesiredVector _vector;
    float _maxSpeed;
    float _maxForce;
    Vector3 aux;
    PathFinding _pathFinding;
    EnemyNormalEntity _enemy;
    Transform _target;
    BaseMovement _base;
    FSM _fsm;

    private float life;
    public PathFindingState(DesiredVector d, float speed, float force, PathFinding p, EnemyNormalEntity enemy, Transform target,BaseMovement b,FSM f)
    {
        _vector = d;
        _maxSpeed = speed;
        _maxForce = force;
        _pathFinding = p;
        _enemy = enemy;
        _target = target;
        _base = b;
        _fsm = f;
    }

    public void OnEnter()
    {
       // setear bool de la animacion a true
    }
    public void OnUpdate()
    {

             

        var dist = _vector.Desired(_target.position, _enemy.transform.position);
        if (dist.magnitude > 3)// radio
        {
            if (_pathFinding.Path.Count <= 0 || Vector3.Distance(_target.transform.position, _pathFinding.Path[_pathFinding.Path.Count - 1].Position) > 4) //distancia mayor al ultimo nodo.
            {
                _pathFinding.FindPath(_enemy.transform.position, _target.transform.position);
                Debug.LogError("arme Recorrido");
            }
            _base.AddForce(MoveWithFinding(_base.Velocity));

        }
        else
        {
            // ir a goto enemy
            _fsm.ChangeStates(EnemyState.ArriveEnemy);

        }










    }

    public void OnExit()
    {
        //setear Bool de la anim a false
      
    }

    public Vector3 MoveWithFinding(Vector3 _velocity)
    {
        if (_pathFinding.Path.Count > currentPathindex)
        {
            Debug.LogError("creo var");

            var targetPos = _pathFinding.Path[currentPathindex].Position;

            if (_vector.Desired(targetPos, _enemy.transform.position).magnitude > 1f)
            {
                var goTo = _vector.Desired(targetPos, _enemy.transform.position);
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


}
