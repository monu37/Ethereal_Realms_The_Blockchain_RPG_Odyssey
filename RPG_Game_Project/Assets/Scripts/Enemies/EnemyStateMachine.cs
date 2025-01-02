using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState CurrentState { get; private set; }

    public void Initialize(EnemyState _startstate)
    {
        CurrentState = _startstate;

        CurrentState.enter();
    }

    

    public void changestate(EnemyState _newstate)
    {
        CurrentState.exit();

        CurrentState=_newstate;

        CurrentState.enter();
    }
}
