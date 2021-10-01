using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class StateDetector
{

    Transform _enemy;
    Transform _target;
    DesiredVector _desired;
    
    float _maxRadius;
    int _speed;
    Transform _obstacleData;
    public StateDetector(Transform enemy,Transform target,int minRadToStop,int speed, DesiredVector de)
    {
        _enemy = enemy;
        _target = target;
        _maxRadius = minRadToStop;
        _speed = speed;
        _desired = de;

        _playertransform = _target;

        _playerRadius = _maxRadius;

    }
    Transform _playertransform;
    float _playerRadius;
    // lo uso para definir el peso que va a tener el comportamiento de seek.
    public int SeekWeight()
    {
        var range = _desired.Desired(_target.position,_enemy.position).magnitude;

        if (range < _maxRadius) return 0;
        else return _speed;
    }
    // en caso de estar cerca de una pared, va a cambiar al target y al radio para que deje de buscar al player porque se está por chochar contra una pared y se guarda cual va a ser el obstaculo.
    // y le da peso al comportamiento de los obstaculos.
    int testing;

    Transform _child;
    public Transform Parent()
    {
        
        foreach ( var datacolect in DataManager.instance.allenviromentData)
        {
           
            if (datacolect.gameObject.layer == 13)
            {
                
                _child = datacolect.GetChild(0);
                _child.position = _enemy.transform.position;
                if (Mathf.Abs(_child.localPosition.x) <= 0.8f && Mathf.Abs(_child.localPosition.z) <= 0.8f)
                {
                    _obstacleData = _child.parent;

                    
                 
                    return  _obstacleData;
                }
               
               // else return  testing = 0;
               
            }
           
        }
        return _obstacleData;
    }   

    public int ObstacleWeight(Transform t)
    {

      
        if (t == null) return 0;
        _child = t.GetChild(0);
        _child.position = _enemy.transform.position;

        if (Mathf.Abs(_child.localPosition.x) <= 0.8f && Mathf.Abs(_child.localPosition.z) <= 0.8f)
        {
          
            _target = _child;
            _maxRadius = _desired.Desired(_child.parent.transform.position, _child.transform.position).magnitude;
            testing = _speed;
           

          


            return testing;
        }
        else
        {
            _obstacleData = null;
            _target = _playertransform;
            _maxRadius = _playerRadius;

            

            return 0;
        }
    }


    //Simplemente se guarda cual el obstaculo para que se lo pase a quien lo necesite
    public Transform ObstacleData
    {
        get
        {
            return _obstacleData;
        }
    }


}
