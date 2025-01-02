using UnityEngine;


public class ShadyDeadState : EnemyState
{

    private EnemyShady enemy;
    public ShadyDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }


    public override void enter()
    {
        base.enter();


    }

    public override void update()
    {
        base.update();

        if (IsTriggerCalled)
            enemy.SelfDestroy();
    }
}
