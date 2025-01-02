public class ShadyIdleState : ShadyGroundedState
{
    public ShadyIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void enter()
    {
        base.enter();

        StateTimer = enemy.IdleTime;

    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (StateTimer < 0)
            StateMachine.changestate(enemy.moveState);

    }
}
