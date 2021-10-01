using System.Collections.Generic;
using System;
using UnityEngine;

namespace CharControl 
{

    public class Control 
    {

        Joystick _joystickLeft;
        Joystick _joystickRight;
        
        Movement _movement;
        Action<Vector3> _ArtificialMovementJoys;
        Action<Vector3> _ArtificialAim;

        public Vector3 _DataJoystickLeft;
        public Vector3 _DataJoystickRight;

        PlayerEntity _player;
        WeaponEntity _weapon;

       
        float _speed;



        public Control(Joystick left, Joystick Right, Movement m, float s, PlayerEntity p,WeaponEntity weapon)
        {
           
            _joystickLeft = left;
            _joystickRight = Right;
            _movement = m;
             _speed = s;
            _player = p;
            _weapon = weapon;

            _ArtificialMovementJoys = _movement.MovementDir;
            _ArtificialMovementJoys += _movement.Facedirection;

            _ArtificialAim = InitialRightJoys;
        }

        public void OnStart()
        {

            _joystickLeft.Ondrag += Dragging; 
            _joystickLeft.OnDragup += NotDragging;

            _joystickRight.Ondrag += DraggingRight;
            _joystickRight.OnDragup += NotDraggingRight;

            //hacer el Ondestroy en player para que no lo siga haciendo en caso de que el player se destruya// rompa
        }
        public void Ondestroy()
        {
            _joystickRight.OnDragup -= NotDraggingRight;
            _joystickRight.OnDragup -= NotDragging;
            _joystickLeft.Ondrag -= Dragging;
            _joystickLeft.OnDragup -= NotDragging;


        }
        private void Dragging(Vector3 pos)
        {
            _ArtificialMovementJoys(pos);
         

        }
        private void NotDragging(Vector3 pos)
        {
            _ArtificialMovementJoys(pos);
           
        }
        private void DraggingRight(Vector3 pos)
        {
            _movement._speed = _speed *0.5f;
            _ArtificialAim(pos);


            _weapon._isShooting = true;
           
        }

       
        private void NotDraggingRight(Vector3 pos)
        {
            _ArtificialAim = InitialRightJoys;
            _ArtificialMovementJoys += _movement.Facedirection;

           
            _weapon._isShooting = false;
            _movement._speed = _speed;
           
        }
         
        void InitialRightJoys(Vector3 pos)
        {
            _ArtificialAim = UpdateRightDirection;
            _ArtificialMovementJoys -= _movement.Facedirection;
            
        }

        void UpdateRightDirection(Vector3 pos)
        {
            _movement.Facedirection(pos);
        }


        //Observado
        
    }



}


