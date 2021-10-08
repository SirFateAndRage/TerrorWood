using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
   PathFinding,
   ArriveEnemy,
   Attack,
   ReciveDmg,
   Die
}
public class FSM
{
    IState _currentState = new EmptyState();
    public List<IState> stateList = new List<IState>();

    Dictionary<EnemyState, IState> _stateDictionary = new Dictionary<EnemyState, IState>();


    public void OnUpdate()
    {
        if (_currentState != null)
            _currentState.OnUpdate();
    }


    public void ChangeStates(EnemyState state)
    {
        if (!_stateDictionary.ContainsKey(state)) return;


        _currentState.OnExit();
        _currentState = _stateDictionary[state];
        _currentState.OnEnter();
    }

    public void AddState(EnemyState key, IState state)
    {
        if (_stateDictionary.ContainsKey(key)) return;

        _stateDictionary.Add(key, state);
    }

    public void RemoveState(EnemyState key)
    {
        if (_stateDictionary.ContainsKey(key))
            _stateDictionary.Remove(key);
    }

}
