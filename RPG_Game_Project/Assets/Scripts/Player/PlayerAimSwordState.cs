
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : playerstate
{
    public PlayerAimSwordState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SkillManager.Sword.dotactive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("busyfor", .2f);
    }

    public override void Update()
    {
        base.Update();

        player.setzerovelocity();

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            StateMachine.ChangeState(player.IdleState);
        }

        Vector2 mousepos= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(player.transform.position.x  >mousepos.x && player.FacingDir == 1)
        {
            player.flip();
        }
        else if (player.transform.position.x < mousepos.x && player.FacingDir == -1)
        {
            player.flip();
        }
    }
}
