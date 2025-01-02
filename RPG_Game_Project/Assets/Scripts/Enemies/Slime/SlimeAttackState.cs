using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
     EnemySlime enemy;

    public SlimeAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

    }

    public override void exit()
    {
        base.exit();

        enemy.LastTimeAttack = Time.time;

    }

    public override void update()
    {
        base.update();

        enemy.setzerovelocity();

        if (IsTriggerCalled)
        {
            StateMachine.changestate(enemy.BattleState);
        }
    }
}
