using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine StateMachine;
    protected Enemy EnemyBase;
    protected Rigidbody2D Rb;


    protected string AnimBoolName;

    protected float StateTimer;
    protected bool IsTriggerCalled;

    public EnemyState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname)
    {
        this.EnemyBase = _enemybase;
        this.StateMachine = _statemachine;
        this.AnimBoolName = _animboolname;

    }

    public virtual void update()
    {
        StateTimer -= Time.deltaTime;
    }

    public virtual void enter()
    {
        IsTriggerCalled = false;
        Rb = EnemyBase.Rb;
        EnemyBase.Anim.SetBool(AnimBoolName, true);
    }

    public virtual void exit()
    {
        EnemyBase.Anim.SetBool(AnimBoolName, false);

        EnemyBase.assignlastanimname(AnimBoolName);

    }

    public virtual void animationfinishtrigger()
    {
        IsTriggerCalled = true;
    }

    

        
}
