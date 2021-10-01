using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CharControl;

public class PlayerEntity : CombatEntity
{
    [SerializeField]
    Joystick left_joystick;
    [SerializeField]
    Joystick right_joystick;
    [SerializeField]
    float _mSpeed;

    IPowerUp _currentPowerUp;
    

    [SerializeField]
    GameObject PlayerModel;


   public  Action StatePlayer;

   public Control _control;
   Movement _movement;
    [SerializeField]
    WeaponEntity _weapon;

    public GameObject Model() { return PlayerModel; }
    

    
    
    bool _alive;
    protected override void Awake()
    {
        base.Awake();
       
    }
    protected override void Start()
    {

        base.Start();
        _movement = new Movement(this,_mSpeed);
        _control = new Control(left_joystick, right_joystick, _movement, _mSpeed,this,_weapon);
        _control.OnStart();



        DataReturn(this.transform);

        //StatePlayer =MoovingNotShooting;
    }

    private void OnDestroy()
    {
        if(left_joystick != null || right_joystick !=null)
        _control.Ondestroy();
    }
    protected override void FixedUpdate()
    {
        _movement.OnUpdate();

    }
    protected override void Update()
    {
        _movement.OnFixedUpdate();
       
    }

}
