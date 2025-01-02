using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerBattleState : EnemyState
{
    private EnemyDeathBringer enemy;
    private Transform player;
    private int moveDir;

    public DeathBringerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyDeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        player = PlayerManager.instance.player.transform;

        //if (player.GetComponent<PlayerStats>().isDead)
            //stateMachine.changestate(enemy.moveState);


    }

    public override void update()
    {
        base.update();

        if (enemy.IsPlayerDetected())
        {
            StateTimer = enemy.BattleTime;

            if (enemy.IsPlayerDetected().distance < enemy.AttackDistance)
            {
                if (CanAttack())
                    StateMachine.changestate(enemy.attackState);
                else
                    StateMachine.changestate(enemy.idleState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.AttackDistance - .1f)
            return;

        enemy.setvelocity(enemy.MoveSpeed * moveDir, Rb.velocity.y);
    }

    public override void exit()
    {
        base.exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.LastTimeAttack + enemy.AttackCoolDown)
        {
            enemy.AttackCoolDown = Random.Range(enemy.MinAttackCoolDown, enemy.MaxAttackCoolDown);
            enemy.LastTimeAttack = Time.time;
            return true;
        }

        return false;
    }
}
