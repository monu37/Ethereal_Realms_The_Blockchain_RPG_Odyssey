using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    EnemySkeleton enemy;

    public SkeletonDeadState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname, EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        enemy.Anim.SetBool(enemy.LastAnimBoolName, true);
        enemy.Anim.speed = 0;
        enemy.Cd.enabled = false;


        StateTimer = .15f;
    }

    public override void update()
    {
        base.update();


        if(StateTimer > 0)
        {
            Rb.velocity = new Vector2(0, 10);
        }
    }
}
