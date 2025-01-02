using System.Collections;
using UnityEngine;

public class DeathBringerAttackState : EnemyState
{
    private EnemyDeathBringer enemy;
    public DeathBringerAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyDeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        enemy.chanceToTeleport += 5;
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
            if (enemy.CanTeleport())
                StateMachine.changestate(enemy.teleportState);
            else
                StateMachine.changestate(enemy.battleState);
                
        }
    }
}
