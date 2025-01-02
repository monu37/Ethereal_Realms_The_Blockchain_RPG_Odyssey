using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.setzerovelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(XInput == player.FacingDir && player.IsWallDetected())
        {
            return;
        }

        if (XInput != 0 && !player.IsBusy)
        {
            StateMachine.ChangeState(player.MoveState);
        }


    }
}
