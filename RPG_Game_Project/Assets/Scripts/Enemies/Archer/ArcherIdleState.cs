using System.Collections;
using UnityEngine;

public class ArcherIdleState : ArcherGroundedState
{
    public ArcherIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }
    public override void enter()
    {
        base.enter();

        StateTimer = enemy.IdleTime;

    }

    public override void exit()
    {
        base.exit();

        AudioManager.instance.playsfx(24, enemy.transform);
    }

    public override void update()
    {
        base.update();

        if (StateTimer < 0)
            StateMachine.changestate(enemy.MoveState);

    }
}
