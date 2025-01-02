using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : playerstate
{
    public PlayerJumpState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Rb.velocity = new Vector2(Rb.velocity.x, player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Rb.velocity.y < 0)
        {
            StateMachine.ChangeState(player.AirState);
        }
    }
}
