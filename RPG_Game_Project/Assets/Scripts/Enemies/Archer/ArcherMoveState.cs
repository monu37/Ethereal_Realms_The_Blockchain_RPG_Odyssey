using System.Collections;
using UnityEngine;


public class ArcherMoveState : ArcherGroundedState
{
    public ArcherMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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

        enemy.setvelocity(enemy.MoveSpeed * enemy.FacingDir, Rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.flip();
            StateMachine.changestate(enemy.IdleState);
        }

    }
}
