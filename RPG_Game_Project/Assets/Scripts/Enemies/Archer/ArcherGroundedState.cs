using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGroundedState : EnemyState
{
    protected Transform player;
    protected EnemyArcher enemy;

    public ArcherGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < enemy.AgroDistance)
        {
            StateMachine.changestate(enemy.BattleState);
        }
    }
}
