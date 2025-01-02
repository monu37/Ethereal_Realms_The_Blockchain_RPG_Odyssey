using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.playsfx(14, null);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.stopsfx(14);
    }

    public override void Update()
    {
        base.Update();

        player.setvelocity(XInput * player.MoveSpeed, Rb.velocity.y);

        if(XInput == 0 || player.IsWallDetected())
        {
            StateMachine.ChangeState(player.IdleState);
        }
    }

}
