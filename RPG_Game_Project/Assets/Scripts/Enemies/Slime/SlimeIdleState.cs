using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeGroundedState
{
    public SlimeIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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

    }

    public override void update()
    {
        base.update();

        if (StateTimer < 0)
        {
            StateMachine.changestate(Enemy.MoveState);
        }
    }
}
