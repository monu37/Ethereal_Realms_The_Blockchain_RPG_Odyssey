using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : playerstate
{
    public PlayerWallSlideState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
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

        if(player.IsWallDetected() ==false)
        {
            StateMachine.ChangeState(player.AirState);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(player.WallJumpState);
            return;
        }


        if (XInput != 0 && player.FacingDir != XInput)
        {
            StateMachine.ChangeState(player.IdleState);
        }

        if (YInput < 0)
        {
            Rb.velocity = new Vector2(0,Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y * .7f);
        }
       

        if (player.IsGroundDetected())
        {
            StateMachine.ChangeState(player.IdleState);
        }
    }
}
