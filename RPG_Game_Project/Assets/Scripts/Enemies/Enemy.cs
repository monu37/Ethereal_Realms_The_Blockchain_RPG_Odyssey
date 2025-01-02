using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFx))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{
    [SerializeField] protected LayerMask PlayerLayerMask;

    [Header("Stunned Info")]
    public float StunDuration;
    public Vector2 StunDir;
    protected bool IsCanBeStunned;
    [SerializeField] protected GameObject CounterImage;

    [Header("Move Info")]
    public float MoveSpeed;
    float DefaultMoveSpeed;
    public float IdleTime;
    public float BattleTime;


    [Header("Attack Info")]

    public float AgroDistance = 2;
    public float AttackDistance;
    public float AttackCoolDown;
    public float MinAttackCoolDown;
    public float MaxAttackCoolDown;
    [HideInInspector] public float LastTimeAttack;

    public EnemyStateMachine StateMachine { get; private set; }
    public string LastAnimBoolName { get; private set; }


    public EntityFx Fx { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine();

        DefaultMoveSpeed = MoveSpeed;

    }

    protected override void Start()
    {
        base.Start();

        Fx = GetComponent<EntityFx>();
    }

    protected override void Update()
    {
        base.Update();


        StateMachine.CurrentState.update();


    }

    public override void slowentityby(float _slowpercentage, float _slowduration)
    {
        MoveSpeed = MoveSpeed * (1 - _slowpercentage);
        Anim.speed = Anim.speed * (1 - _slowpercentage);

        Invoke(nameof(returndefaultspped), _slowduration);
    }
    protected override void returndefaultspped()
    {
        base.returndefaultspped();

        MoveSpeed = DefaultMoveSpeed;
    }

    public virtual void assignlastanimname(string _animboolname)
    {
        LastAnimBoolName = _animboolname;
    }

    #region freeze timer
    public virtual void freezetimer(bool _istimefrozen)
    {
        if (_istimefrozen)
        {
            MoveSpeed = 0;
            Anim.speed = 0;
        }
        else
        {
            MoveSpeed = DefaultMoveSpeed;
            Anim.speed = 1;
        }
    }
    public virtual void freezetimefor(float _duration) => StartCoroutine(freezetimercoroutine(_duration));

    protected virtual IEnumerator freezetimercoroutine(float _second)
    {
        freezetimer(true);

        yield return new WaitForSeconds(_second);

        freezetimer(false);
    }

    #endregion

    #region counter attack window
    public virtual void opencounterattackwindow()
    {
        IsCanBeStunned = true;

        CounterImage.SetActive(true);
    }

    public virtual void closecounterattackwindow()
    {
        IsCanBeStunned = false;
        CounterImage.SetActive(false);
    }
    #endregion

    public virtual bool CheckCanBeStunned()
    {
        if (IsCanBeStunned)
        {
            closecounterattackwindow();
            return true;
        }
        return false;
    }

    public virtual void animationspecialattacktrigger()
    {

    }

    public virtual void animationfinishtrigger() => StateMachine.CurrentState.animationfinishtrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(WallCheckTrans.position, Vector2.right * FacingDir, 50, PlayerLayerMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + AttackDistance * FacingDir, transform.position.y));
    }
}
