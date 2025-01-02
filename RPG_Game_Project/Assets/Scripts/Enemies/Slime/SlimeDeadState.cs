using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : EnemyState
{
    private EnemySlime enemy;
    public SlimeDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }


    public override void enter()
    {
        base.enter();

        enemy.Anim.SetBool(enemy.LastAnimBoolName, true);
        enemy.Anim.speed = 0;
        enemy.Cd.enabled = false;

        StateTimer = .15f;
    }

    public override void update()
    {
        base.update();

        if (StateTimer > 0)
            Rb.velocity = new Vector2(0, 10);
    }
}
