using UnityEngine;


public class DeathBringerSpellCastState : EnemyState
{
    private EnemyDeathBringer enemy;

    private int amountOfSpells;
    private float spellTimer;

    public DeathBringerSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyDeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void enter()
    {
        base.enter();

        amountOfSpells = enemy.amountOfSpells;
        spellTimer = .5f;
    }

    public override void update()
    {
        base.update();

        spellTimer -= Time.deltaTime;

        if (CanCast())
            enemy.CastSpell();


        if (amountOfSpells <= 0)
            StateMachine.changestate(enemy.teleportState);
    }

    public override void exit()
    {
        base.exit();

        enemy.lastTimeCast = Time.time;
    }

    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCooldown;
            return true;
        }

        return false;
    }
}
