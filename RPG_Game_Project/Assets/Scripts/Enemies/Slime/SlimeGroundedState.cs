using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundedState : EnemyState
{
    protected EnemySlime Enemy;
    protected Transform Player;

    public SlimeGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.Enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        Player = PlayerManager.instance.player.transform;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (Enemy.IsPlayerDetected() || Vector2.Distance(Enemy.transform.position, Player.position) < 2)
        {
            StateMachine.changestate(Enemy.BattleState);
        }
    }
}
