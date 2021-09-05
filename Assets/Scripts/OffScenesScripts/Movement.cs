using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharControl;

public class Movement
{
    PlayerEntity _player;
    Control _control;

   

    public Movement(PlayerEntity p,Control c)
    {
        _player = p;
        _control = c;
    }

    public void  MovementDir(float speed,Vector3 data)
    {
        _player.transform.LookAt(_player.transform.position + _control._DataJoystickLeft);
        _player.Model().transform.LookAt(_player.Model().transform.position + data);
        _player.rb.MovePosition(_player.rb.position + _player.transform.forward * _control._DataJoystickLeft.magnitude * speed * Time.deltaTime);
      
    }

}

