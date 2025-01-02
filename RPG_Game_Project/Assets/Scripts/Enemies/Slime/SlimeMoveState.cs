using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : SlimeGroundedState
{
    public SlimeMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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
