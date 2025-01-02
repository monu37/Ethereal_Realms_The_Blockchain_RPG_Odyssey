using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeSkill : playerstate
{
    public PlayerBlackholeSkill(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void animationfinishtrigger()
    {
        base.animationfinishtrigger();
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
    }
}
