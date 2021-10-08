using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class ArriveState : IState
{

    DesiredVector _vec;
    FSM _fsm;
    EnemyNormalEntity _enemy;
    CombatEntity _target;
    BaseMovement _base;
    float _mSpeed;
    float _mForce;


  

    public void OnEnter()
    {
       
    }

    public void OnUpdate()
    {
        var dist = _vec.Desired(_target.transform.position, _enemy.transform.position);
        if(dist.magnitude > 1 )
        {
            if (dist.magnitude > 4) _fsm.ChangeStates(EnemyState.PathFinding);

            SeekTo(_target.transform);
        }
        else
        {
            _fsm.ChangeStates(EnemyState.Attack);
        }

    }

    public void OnExit()
    {
       
    }


     Vector3 SeekTo(Transform t)
     {
        Vector3 desired = _vec.Desired(t.position, _enemy.transform.position);
        desired.Normalize();
        desired *= _mSpeed;
        return _vec.Steering(desired, _base.Velocity, _mForce);

     }
    
}
