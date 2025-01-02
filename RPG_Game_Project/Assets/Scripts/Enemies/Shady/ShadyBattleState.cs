using UnityEngine;


public class ShadyBattleState : EnemyState
{

    private Transform player;
    private EnemyShady enemy;
    private int moveDir;

    private float defaultSpeed;

    public ShadyBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
  
        this.enemy = _enemy;
    }


    public override void enter()
    {
        base.enter();

        defaultSpeed = enemy.MoveSpeed;

        enemy.MoveSpeed = enemy.battleStateMoveSpeed;

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().IsDead)
            StateMachine.changestate(enemy.moveState);


    }

    public override void update()
    {
        base.update();

        if (enemy.IsPlayerDetected())
        {
            StateTimer = enemy.BattleTime;

            if (enemy.IsPlayerDetected().distance < enemy.AttackDistance)
                enemy.Stats.killentity(); // this enteres dead state which triggers explosion + drop items and souls
                
        }
        else
        {
            if (StateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                StateMachine.changestate(enemy.idleState);
        }



        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.setvelocity(enemy.MoveSpeed * moveDir, Rb.velocity.y);
    }

    public override void exit()
    {
        base.exit();

        enemy. MoveSpeed = defaultSpeed;
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
}
