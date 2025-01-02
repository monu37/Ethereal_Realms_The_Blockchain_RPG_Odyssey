using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : skeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname, EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname, _enemy)
    {
    }

    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        Enemy.setvelocity(Enemy.MoveSpeed * Enemy.FacingDir, Rb.velocity.y);

        if (Enemy.IsWallDetected() || !Enemy.IsGroundDetected())
        {
            Enemy.flip();
            StateMachine.changestate(Enemy.IdleState);
        }
    }
}
