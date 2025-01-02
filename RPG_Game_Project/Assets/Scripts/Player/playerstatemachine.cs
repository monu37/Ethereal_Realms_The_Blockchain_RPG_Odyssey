using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstatemachine 
{
    public playerstate CurrentState { get; private set; }

   public void Initialize(playerstate _startstate)
    {
        CurrentState = _startstate;
        CurrentState.Enter();
    }

    public void ChangeState(playerstate _newstate)
    {
        CurrentState.Exit();

        CurrentState = _newstate;
        CurrentState.Enter();
    }
}
