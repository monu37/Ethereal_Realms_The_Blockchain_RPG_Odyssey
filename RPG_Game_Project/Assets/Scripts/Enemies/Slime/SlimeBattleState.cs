using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBattleState : EnemyState
{
    private EnemySlime enemy;
    private Transform PlayerTrans;
    private int moveDir;

    public SlimeBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }


    public override void enter()
    {
        base.enter();

        PlayerTrans = PlayerManager.instance.player.transform;

        if (PlayerTrans.GetComponent<PlayerStats>().IsDead)
        {
            StateMachine.changestate(enemy.MoveState);
        }
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
                {
                    StateMachine.changestate(enemy.AttackState);
                }

            }
        }
        else
        {
            if (StateTimer < 0 || Vector2.Distance(PlayerTrans.position, enemy.transform.position) > 7)
            {
                StateMachine.changestate(enemy.IdleState);
            }
        }


        if (PlayerTrans.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (PlayerTrans.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }


        enemy.setvelocity(enemy.MoveSpeed * moveDir, Rb.velocity.y);
    }

    public override void exit()
    {
        base.exit();
    }

   
    bool CanAttack()
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
