using UnityEngine;


public class DeathBringerDeadState : EnemyState
{
    private EnemyDeathBringer enemy;

    public DeathBringerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyDeathBringer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
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
