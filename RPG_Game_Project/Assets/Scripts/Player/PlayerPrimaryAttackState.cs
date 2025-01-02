using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrimaryAttackState : playerstate
{
    public int ComboCounter {  get; private set; }
    float LastTimeAttacked;
    float ComboWindow = 2;



    public PlayerPrimaryAttackState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //AudioManager.instance.playsfx(2);

        XInput = 0; 

        if(ComboCounter >2 || Time.time >= LastTimeAttacked + ComboWindow)
        {
            ComboCounter = 0;
        }

        player.Anim.SetInteger("ComboCounter", ComboCounter);


        #region Choose Attack Direction
        float AttackDir = player.FacingDir;

        if(XInput !=0 )
        {
            AttackDir = XInput;
        }

        #endregion

        player.setvelocity(player.AttackMovement[ComboCounter].x * AttackDir, player.AttackMovement[ComboCounter].y);



        LastTimeAttacked = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("busyfor", .15f);

        ComboCounter++;

        LastTimeAttacked=Time.time;;
    }

    public override void Update()
    {
        base.Update();

        if(StateTimer < 0)
        {
            player.setzerovelocity();
        }

        if (IsTriggerCalled)
        {
            StateMachine.ChangeState(player.IdleState);
        }
    }
}
