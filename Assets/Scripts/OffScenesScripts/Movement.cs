using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharControl;

public class Movement
{
    PlayerEntity _player;

    Vector3 _Direction;
    Vector3 _FaceDirection;
     public float _speed;




    public Movement(PlayerEntity p,float s)
    {
        _player = p;
        _speed = s;

    }

    public void MovementDir(Vector3 data)
    {

        _Direction = data;

    }
    public void Facedirection(Vector3 data)
    {
        _FaceDirection = data;

    }

    public void OnUpdate()
    {
        _player.rb.MovePosition(_player.rb.position + _player.transform.forward * _Direction.magnitude * _speed * Time.deltaTime);

        _player.transform.LookAt(_player.transform.position + _Direction);

    }

    public void OnFixedUpdate()
    {
        _player.Model().transform.LookAt(_player.transform.position + _FaceDirection);
    }
}

