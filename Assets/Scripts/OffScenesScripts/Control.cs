using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharControl 
{

    public class Control
    {
        PlayerEntity _player;
        
         public Vector3 _DataJoystickLeft;
         public Vector3 _DataJoystickRight;

        public Control(PlayerEntity p)
        {
            _player = p;
            
        }

        public void OnFixedUpdate()
        {
            if (_DataJoystickRight != Vector3.zero)
                _player.StateChecker(true);
            else _player.StateChecker(false);
        }

        public void OnUpdate()
        {
            _DataJoystickLeft.x = _player.Left().Horizontal;
            _DataJoystickLeft.z = _player.Left().Vertical;
            _DataJoystickRight.x = _player.Right().Horizontal;
            _DataJoystickRight.z = _player.Right().Vertical;
        }
    }



}


