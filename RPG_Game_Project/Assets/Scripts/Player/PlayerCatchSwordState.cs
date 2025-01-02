using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : playerstate
{

    Transform SwordTrans;
    public PlayerCatchSwordState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SwordTrans = player.Sword.transform;

        player.Fx.playdustfx();
        player.Fx.screenshake(player.Fx.ShakeSwordImpact);

        if (player.transform.position.x > SwordTrans.position.x && player.FacingDir == 1)
        {
            player.flip();
        }
        else if (player.transform.position.x < SwordTrans.position.x && player.FacingDir == -1)
        {
            player.flip();
        }

        Rb.velocity = new Vector2(player.SwordReturnForce * -player.FacingDir, Rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("busyfor", .1f);
    }

    public override void Update()
    {
        base.Update();

        if (IsTriggerCalled)
        {
            StateMachine.ChangeState(player.IdleState);
        }
    }
}
