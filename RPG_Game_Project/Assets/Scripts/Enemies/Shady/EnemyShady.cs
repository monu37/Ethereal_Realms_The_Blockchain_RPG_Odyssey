using JetBrains.Annotations;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyShady : Enemy
{

    [Header("Shady spesifics")]
    public float battleStateMoveSpeed;

    [SerializeField] private GameObject explosivePrefab;
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxSize;


    #region States

    public ShadyIdleState idleState { get; private set; }
    public ShadyMoveState moveState { get; private set; }
    public ShadyDeadState deadState { get; private set; }
    public ShadyStunnedState stunnedState { get; private set; }
    public ShadyBattleState battleState { get; private set; }


    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new ShadyIdleState(this, StateMachine, "Idle", this);
        moveState = new ShadyMoveState(this, StateMachine, "Move", this);

        deadState = new ShadyDeadState(this, StateMachine, "Dead", this);

        stunnedState = new ShadyStunnedState(this, StateMachine, "Stunned",this);
        battleState = new ShadyBattleState(this, StateMachine, "MoveFast", this);
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(idleState);
    }

  
    public override bool CheckCanBeStunned()
    {
        if (base.CheckCanBeStunned())
        {
            StateMachine.changestate(stunnedState);
            return true;
        }

        return false;
    }

    public override void die()
    {
        base.die();
        StateMachine.changestate(deadState);

    }
    protected override void Update()
    {
        base.Update();
    }

 

    public override void animationspecialattacktrigger()
    {
        GameObject newExplosive = Instantiate(explosivePrefab, AttackCheckTrans.position, Quaternion.identity);
        newExplosive.GetComponent<ExplosiveController>().SetupExplosive(Stats, growSpeed, maxSize, AttackCheckRadius);

        Cd.enabled = false;
        Rb.gravityScale = 0;

        
    }

    public void SelfDestroy() => Destroy(gameObject);

}
