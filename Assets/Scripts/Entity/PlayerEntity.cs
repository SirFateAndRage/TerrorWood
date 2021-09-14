using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CharControl;

public class PlayerEntity : CombatEntity, IObservable,IenviromentData
{
    [SerializeField]
    Joystick left_joystick;
    [SerializeField]
    Joystick right_joystick;
    [SerializeField]
    float _mSpeed;
    [SerializeField]
    GameObject PlayerModel;
    


    Action StatePlayer;

    Control _control;
    Movement _movement;

    public Joystick Left() { return left_joystick;}
    public Joystick Right() { return right_joystick; }
    public GameObject Model() { return PlayerModel; }
    

    
    
    bool _alive;
    protected override void Awake() => base.Awake();
    protected override void Start()
    {
        base.Start();
        _control = new Control(this);
        _movement = new Movement(this, _control);
        DataReturn(this.transform);

        StatePlayer =NotShooting;
    }

    protected override void FixedUpdate()
    {
        _control.OnFixedUpdate();
        StatePlayer();
    }
    protected override void Update()
    {
        _control.OnUpdate();
        
    }

    public void StateChecker(bool isshooting)
    {
        
        if (!isshooting)
        {
            StatePlayer = NotShooting;
            
        }
        else StatePlayer = Shooting;
    }

   
    void NotShooting() 
    {
        //NotifyToObservers(Utils.ActionObservers.notshooting);
        _movement.MovementDir(_mSpeed, _control._DataJoystickLeft);
    }

    void Shooting()
    {
        
        NotifyToObservers(Utils.ActionObservers.shooting);
       _movement.MovementDir(_mSpeed * 0.5f, _control._DataJoystickRight);
    }

    List<IObserver> _allObservers = new List<IObserver>();
    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
            _allObservers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
            _allObservers.Remove(obs);
    }

    public void NotifyToObservers(Utils.ActionObservers action)
    {
        for (int i = 0; i < _allObservers.Count; i++)
        {
            _allObservers[i].Notify(action);
        }
    }

    public void DataReturn(Transform t)
    {
        GameManager.instance.AddEnviromentData(t);
    }
}
