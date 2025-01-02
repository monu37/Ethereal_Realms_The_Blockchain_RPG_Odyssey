using System.Collections;
using UnityEngine;


public class ShadyMoveState : ShadyGroundedState
{
    public ShadyMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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
            StateMachine.changestate(enemy.idleState);
        }

    }
}


