using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    EnemySkeleton enemy;

    public SkeletonStunState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname, EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        enemy.Fx.InvokeRepeating("redcolorblink", 0, .08f);

        StateTimer = enemy.StunDuration;
        Rb.velocity = new Vector2(-enemy.FacingDir * enemy.StunDir.x, enemy.StunDir.y);
    }

    public override void exit()
    {
        base.exit();

        enemy.Fx.Invoke("cancelcolorchange", 0);
    }

    public override void update()
    {
        base.update();

        if(StateTimer < 0)
        {
            StateMachine.changestate(enemy.IdleState);
        }
    }
}
