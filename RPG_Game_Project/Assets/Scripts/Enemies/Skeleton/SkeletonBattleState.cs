using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    Transform PlayerTrans;

    EnemySkeleton Enemy;

    int MoveDir;

    public SkeletonBattleState(Enemy _enemybase, EnemyStateMachine _statemachine, string _animboolname, EnemySkeleton _enemy) : base(_enemybase, _statemachine, _animboolname)
    {
       this.Enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        PlayerTrans = PlayerManager.instance.player.transform;

        if (PlayerTrans.GetComponent<PlayerStats>().IsDead)
        {
            StateMachine.changestate(Enemy.MoveState);
        }
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if(Enemy.IsPlayerDetected())
        {
            StateTimer = Enemy.BattleTime;

            if(Enemy.IsPlayerDetected().distance < Enemy.AttackDistance)
            {
                if (CanAttack())
                {
                    StateMachine.changestate(Enemy.AttackState);
                }
               
            }
        }
        else
        {
            if(StateTimer < 0 || Vector2.Distance(PlayerTrans.position,Enemy.transform.position) > 7)
            {
                StateMachine.changestate(Enemy.IdleState);
            }
        }


        if(PlayerTrans.position.x > Enemy.transform.position.x)
        {
            MoveDir = 1;
        }
        else if (PlayerTrans.position.x < Enemy.transform.position.x)
        {
            MoveDir = -1;
        }


        Enemy.setvelocity(Enemy.MoveSpeed * MoveDir, Rb.velocity.y);
    }

    bool CanAttack()
    {
        if(Time.time >= Enemy.LastTimeAttack + Enemy.AttackCoolDown)
        {

            Enemy.AttackCoolDown=Random.Range(Enemy.MinAttackCoolDown,Enemy.MaxAttackCoolDown);

            Enemy.LastTimeAttack= Time.time;
            return true;
        }
        return false;
    }
}
