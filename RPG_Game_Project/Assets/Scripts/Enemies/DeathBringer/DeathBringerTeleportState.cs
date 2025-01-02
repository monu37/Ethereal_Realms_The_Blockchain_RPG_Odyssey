using System.Collections;
using UnityEngine;

public class DeathBringerTeleportState : EnemyState
{

    private EnemyDeathBringer enemy;

    public DeathBringerTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyDeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        enemy.Stats.makeinvincible(true);
    }

    public override void update()
    {
        base.update();

        if (IsTriggerCalled)
        {
            if (enemy.CanDoSpellCast())
                StateMachine.changestate(enemy.spellCastState);
            else
                StateMachine.changestate(enemy.battleState);

           
        }

        
    }

    public override void exit()
    {
        base.exit();

        enemy.Stats.makeinvincible(false);
    }
}
