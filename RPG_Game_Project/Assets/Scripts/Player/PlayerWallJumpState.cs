using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : playerstate
{
    public PlayerWallJumpState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateTimer = 1f;
        player.setvelocity(5 * -player.FacingDir, player.JumpForce);
    }

   
    public override void Exit()
    {
        base.Exit();
    }

  
    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
            StateMachine.ChangeState(player.AirState);
        }

        if (player.IsGroundDetected())
        {
            StateMachine.ChangeState(player.IdleState);
        }
    }

   
}
