using System.Collections;
using UnityEngine;


public class ArcherDeadState : EnemyState
{
    private EnemyArcher enemy;

    public ArcherDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyArcher _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
