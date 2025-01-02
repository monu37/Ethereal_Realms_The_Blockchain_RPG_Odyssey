using UnityEngine;


public class ShadyStunnedState : EnemyState
{
    private EnemyShady enemy;
    public ShadyStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        enemy.Fx.Invoke("cancelcolorchange", 0);
    }

    public override void update()
    {
        base.update();

        if (StateTimer < 0)
            StateMachine.changestate(enemy.idleState);
    }
}
