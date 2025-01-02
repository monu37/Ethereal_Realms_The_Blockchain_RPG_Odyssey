using UnityEngine;


public class ShadyGroundedState : EnemyState
{

    protected Transform player;
    protected EnemyShady enemy;
    public ShadyGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < enemy.AgroDistance)
        {
            StateMachine.changestate(enemy.battleState);
        }
    }
}