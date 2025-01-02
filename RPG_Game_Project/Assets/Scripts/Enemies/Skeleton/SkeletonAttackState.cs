using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{

    EnemySkeleton enemy;
    public SkeletonAttackState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname,EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname)
    {
        enemy = _enemy;

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

        if(IsTriggerCalled)
        {
            StateMachine.changestate(enemy.BattleState);
        }
    }
}
