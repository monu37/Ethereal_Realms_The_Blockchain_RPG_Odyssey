using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : skeletonGroundedState
{
    public SkeletonIdleState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname, EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname, _enemy)
    {
    }

    public override void enter()
    {
        base.enter();

        StateTimer = Enemy.IdleTime;
    }

    public override void exit()
    {
        base.exit();

        AudioManager.instance.playsfx(24, Enemy.transform);
    }

    public override void update()
    {
        base.update();

        if (StateTimer <0)
        {
            StateMachine.changestate(Enemy.MoveState);
        }
    }
}
