using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : playerstate
{
    public PlayerGroundedState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.R) && player.SkillManager.Blackhole.IsBlackHoleUnlocked  ) // balck hole
        {
            if(player.SkillManager.Blackhole.CoolDownTimer > 0)
            {
                player.Fx.createpopuptext("Cooldown!");
                return;
            }
            StateMachine.ChangeState(player.BlackholeState);  
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.SkillManager.Sword.IsSwordUnlocked) //sword skills
        {
            StateMachine.ChangeState(player.AimSwordState);
        }

        if(Input.GetKeyUp(KeyCode.Q) && player.SkillManager.Parry.IsParryUnlocked) //parry
        {
            StateMachine.ChangeState(player.CounterAttackState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //primary attack
        {
            StateMachine.ChangeState(player.PrimaryAttackState);
        }

        if (!player.IsGroundDetected()) //air
        {
            StateMachine.ChangeState(player.AirState);
        }

        if (Input.GetKeyDown(KeyCode.Space)&& player.IsGroundDetected()) // jump
        {
            StateMachine.ChangeState(player.JumpState);
        }
    }

    bool HasNoSword()
    {
        if(!player.Sword)
        {
            return true;
        }

        player.Sword.GetComponent<swordSkillController>().returnsword();

        return false;
    }
}
