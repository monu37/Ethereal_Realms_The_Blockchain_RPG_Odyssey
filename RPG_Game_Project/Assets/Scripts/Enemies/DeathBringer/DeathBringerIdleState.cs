using System.Collections;
using UnityEngine;


public class DeathBringerIdleState : EnemyState
{
    private EnemyDeathBringer enemy;
    private Transform player;

    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyDeathBringer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void enter()
    {
        base.enter();

        StateTimer = enemy.IdleTime;
        player = PlayerManager.instance.player.transform;


    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
            enemy.bossFightBegun = true;


        if (Input.GetKeyDown(KeyCode.V))
            StateMachine.changestate(enemy.teleportState);

        if (StateTimer < 0 && enemy.bossFightBegun)
            StateMachine.changestate(enemy.battleState);

    }
}
