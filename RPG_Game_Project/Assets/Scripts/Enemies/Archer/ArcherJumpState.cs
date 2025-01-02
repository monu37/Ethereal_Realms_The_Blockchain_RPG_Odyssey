using System.Collections;
using UnityEngine;


public class ArcherJumpState : EnemyState
{
    private EnemyArcher enemy;
    public ArcherJumpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        Rb.velocity = new Vector2(enemy.jumpVelocity.x * -enemy.FacingDir, enemy.jumpVelocity.y);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        enemy.Anim.SetFloat("yVelocity", Rb.velocity.y);

        if (Rb.velocity.y < 0 && enemy.IsGroundDetected())
            StateMachine.changestate(enemy.BattleState);
    }
}
