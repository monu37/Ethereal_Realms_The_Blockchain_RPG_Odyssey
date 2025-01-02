using System.Collections;
using UnityEngine;


public class ArcherBattleState : EnemyState
{
    private Transform player;
    private EnemyArcher enemy;
    private int moveDir;
    public ArcherBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().IsDead)
            StateMachine.changestate(enemy.MoveState);


    }

    public override void update()
    {
        base.update();

        if (enemy.IsPlayerDetected())
        {
            StateTimer = enemy.BattleTime;

            if (enemy.IsPlayerDetected().distance < enemy.safeDistance)
            {
                if (CanJump())
                    StateMachine.changestate(enemy.JumpState);
            }

            if (enemy.IsPlayerDetected().distance < enemy.AttackDistance)
            {
                if (CanAttack())
                    StateMachine.changestate(enemy.AttackState);
            }
        }
        else
        {
            if (StateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                StateMachine.changestate(enemy.IdleState);
        }


        BattleStateFlipControll();
    }

    private void BattleStateFlipControll()
    {
        if (player.position.x > enemy.transform.position.x && enemy.FacingDir == -1)
            enemy.flip();
        else if (player.position.x < enemy.transform.position.x && enemy.FacingDir == 1)
            enemy.flip();
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

    private bool CanJump()
    {
        if (enemy.GroundBehind() == false || enemy.WallBehind() == true)
            return false;


        if (Time.time >= enemy.lastTimeJumped + enemy.jumpCooldown)
        {

            enemy.lastTimeJumped = Time.time;
            return true;
        }

        return false;
    }
}


