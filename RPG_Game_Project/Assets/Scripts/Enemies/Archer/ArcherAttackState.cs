using System.Collections;
using UnityEngine;

public class ArcherAttackState : EnemyState
{
    private EnemyArcher enemy;
    public ArcherAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
            StateMachine.changestate(enemy.BattleState);
    }
}
