using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathBringer : Enemy
{
    #region States
    public DeathBringerBattleState battleState { get ; private set; }
    public DeathBringerAttackState attackState { get ; private set; }
    public DeathBringerIdleState idleState { get ; private set; }
    public DeathBringerDeadState deadState { get ; private set; }
    public DeathBringerSpellCastState spellCastState { get ; private set; }
    public DeathBringerTeleportState teleportState { get ; private set; }

    #endregion
    public bool bossFightBegun;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

    protected override void Awake()
    {
        base.Awake();

        setupdefailtfacingdir(-1);

        idleState = new DeathBringerIdleState(this, StateMachine, "Idle", this);

        battleState = new DeathBringerBattleState(this, StateMachine, "Move", this);
        attackState = new DeathBringerAttackState(this, StateMachine, "Attack", this);

        deadState = new DeathBringerDeadState(this, StateMachine, "Idle", this);
        spellCastState = new DeathBringerSpellCastState(this, StateMachine, "SpellCast", this);
        teleportState = new DeathBringerTeleportState(this, StateMachine, "Teleport", this);
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void die()
    {
        base.die();
        StateMachine.changestate(deadState);

    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;


        float xOffset = 0;

        if (player.Rb.velocity.x != 0)
            xOffset = player.FacingDir * spellOffset.x;

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpellController>().SetupSpell(Stats);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (Cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
            //Debug.Log("Looking for new position");
            FindPosition();
        }
    }


    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, GroundLayerMask);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, GroundLayerMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }


    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }


        return false;
    }

    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
            return true;
        }

        return false;
    }
}
