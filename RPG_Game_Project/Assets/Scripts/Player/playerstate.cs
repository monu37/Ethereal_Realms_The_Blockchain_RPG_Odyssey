using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstate 
{
    protected playerstatemachine StateMachine;
    protected Player player;

    protected Rigidbody2D Rb;
    string AnimBoolName;

    protected float XInput;
    protected float YInput;

    protected float StateTimer;

    protected bool IsTriggerCalled;

    public playerstate(Player _player, playerstatemachine _statemachine, string _animBoolName)
    {
        this.player = _player;
        this.StateMachine = _statemachine;
        this.AnimBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(AnimBoolName,true);
        Rb = player.Rb;

        IsTriggerCalled = false;
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(AnimBoolName, false);

    }

    public virtual void Update()
    {
        StateTimer -= Time.deltaTime;

        XInput = Input.GetAxisRaw("Horizontal");
        YInput = Input.GetAxisRaw("Vertical");

        player.Anim.SetFloat("YVelocity", Rb.velocity.y);
    }

    public virtual void animationfinishtrigger()
    {
        IsTriggerCalled=true;
    }

}
