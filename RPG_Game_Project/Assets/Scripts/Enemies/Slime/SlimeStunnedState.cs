using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStunnedState : EnemyState
{
    private EnemySlime enemy;
    public SlimeStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        enemy.Fx.InvokeRepeating("redcolorblink", 0, .1f);

        StateTimer = enemy.StunDuration;
        Rb.velocity = new Vector2(-enemy.FacingDir * enemy.StunDir.x, enemy.StunDir.y);
    }

    public override void exit()
    {
        base.exit();

        enemy.Stats.makeinvincible(false);
    }

    public override void update()
    {
        base.update();

        if(Rb.velocity.y > .1f && enemy.IsGroundDetected())
        {
            enemy.Fx.Invoke("cancelcolorchange", 0);
            enemy.Anim.SetTrigger("StunFold");
            enemy.Stats.makeinvincible(true);
        }

        if (StateTimer < 0)
        {
            StateMachine.changestate(enemy.IdleState);
        }
    }
}

