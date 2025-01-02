using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{

    [Header("Archer spisifc info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowDamage;

    public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance; // how close palyer should be to trigger jump on battle state
    [HideInInspector] public float lastTimeJumped;

    [Header("Additional collision check")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;


    #region States

    public ArcherIdleState IdleState { get; private set; }
    public ArcherMoveState MoveState { get; private set; }
    public ArcherBattleState BattleState { get; private set; }
    public ArcherAttackState AttackState { get; private set; }
    public ArcherDeadState DeadState { get; private set; }
    public ArcherStunnedState StunnedState { get; private set; }

    public ArcherJumpState JumpState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        IdleState = new ArcherIdleState(this, StateMachine, "Idle", this);
        MoveState = new ArcherMoveState(this, StateMachine, "Move", this);
        BattleState = new ArcherBattleState(this, StateMachine, "Idle", this);
        AttackState = new ArcherAttackState(this, StateMachine, "Attack", this);
        DeadState = new ArcherDeadState(this, StateMachine, "Move", this);
        StunnedState = new ArcherStunnedState(this, StateMachine, "Stunned", this);
        JumpState = new ArcherJumpState(this, StateMachine, "Jump", this);

    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

   
    public override bool CheckCanBeStunned()
    {
        if (base.CheckCanBeStunned())
        {
            StateMachine.changestate(StunnedState);
            return true;
        }

        return false;
    }

    public override void die()
    {
        base.die();
        StateMachine.changestate(DeadState);

    }


    public override void animationspecialattacktrigger()
    {
        GameObject newArrow = Instantiate(arrowPrefab, AttackCheckTrans.position, Quaternion.identity);
        newArrow.GetComponent<ArrowController>().SetupArrow(arrowSpeed * FacingDir, Stats);
    }

    public bool GroundBehind() => Physics2D.BoxCast(groundBehindCheck.position, groundBehindCheckSize, 0, Vector2.zero,0, GroundLayerMask);
    public bool WallBehind() => Physics2D.Raycast(WallCheckTrans.position, Vector2.right * -FacingDir, WallCheckDistance + 2, GroundLayerMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireCube(groundBehindCheck.position,groundBehindCheckSize);
    }
}
