using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonGroundedState : EnemyState
{

    protected EnemySkeleton Enemy;
    protected Transform Player;

    public skeletonGroundedState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname, EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname)
    {
        Enemy = _enemy;
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

        if (Enemy.IsPlayerDetected() || Vector2.Distance(Enemy.transform.position,Player.position) < 2)
        {
            StateMachine.changestate(Enemy.BattleState);
        }
    }

   
}
