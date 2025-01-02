using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : playerstate
{
    public PlayerDashState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        

        player.SkillManager.Dash.cloneondash();

        StateTimer = player.DashDuration;

        player.Stats.makeinvincible(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.SkillManager.Dash.cloneonarrival();
        player.setvelocity(0, Rb.velocity.y);

        player.Stats.makeinvincible(false);


    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            StateMachine.ChangeState(player.wallSlideState);
        }

        player.setvelocity(player.DashSpeed * player.DashDir, 0);
        
        
        if (StateTimer < 0)
        {
            StateMachine.ChangeState(player.IdleState);
        }

        player.Fx.createafterimage();
    }
}
