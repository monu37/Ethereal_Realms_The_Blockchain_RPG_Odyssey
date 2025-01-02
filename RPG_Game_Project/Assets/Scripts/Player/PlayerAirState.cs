using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : playerstate
{
    public PlayerAirState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.IsWallDetected())
        {
            StateMachine.ChangeState(player.wallSlideState);
        }

        if (player.IsGroundDetected())
        {
            StateMachine.ChangeState(player.IdleState);
        }

        if(XInput != 0)
        {
            player.setvelocity(player.MoveSpeed * .8f * XInput, Rb.velocity.y);
        }
    }
}
