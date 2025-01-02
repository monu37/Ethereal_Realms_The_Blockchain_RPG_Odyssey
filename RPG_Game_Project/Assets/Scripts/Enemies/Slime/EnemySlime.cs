using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeType { big,medium,small}
public class EnemySlime : Enemy
{
    [Header("Slime Info")]
    [SerializeField] private SlimeType SlimeType;
    [SerializeField] private int SlimesToCreate;
    [SerializeField] private GameObject SlimePrefab;
    [SerializeField] private Vector2 MinCreationVelocity;
    [SerializeField] private Vector2 MaxCreationVelocity;

    #region state
    public SlimeIdleState IdleState { get; private set; }
    public SlimeMoveState MoveState { get; private set; }
    public SlimeBattleState BattleState { get; private set; }
    public SlimeAttackState AttackState { get; private set; }
    public SlimeStunnedState StunnedState { get; private set; }
    public SlimeDeadState DeadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        setupdefailtfacingdir(-1);

        IdleState = new SlimeIdleState(this, StateMachine, "Idle", this);
        MoveState = new SlimeMoveState(this, StateMachine, "Move", this);
        BattleState = new SlimeBattleState(this, StateMachine, "Move", this);
        AttackState = new SlimeAttackState(this, StateMachine, "Attack", this);

        StunnedState = new SlimeStunnedState(this, StateMachine, "Stunned", this);
        DeadState = new SlimeDeadState(this, StateMachine, "Idle", this);

    }


    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        //if(Input.GetKeyDown(KeyCode.D))
        //    CreateSlimes(slimesToCreate, slimePrefab);
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

        if (SlimeType == SlimeType.small)
            return;

        createslimes(SlimesToCreate, SlimePrefab);

    }

    private void createslimes(int _amountOfSlimes, GameObject _slimePrefab)
    {
        for (int i = 0; i < _amountOfSlimes; i++)
        {
            GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

            newSlime.GetComponent<EnemySlime>().setupslime(FacingDir);
        }
    }

    public void setupslime(int _facingDir)
    {

        if (_facingDir != FacingDir)
            flip();

        float xVelocity = Random.Range(MinCreationVelocity.x, MaxCreationVelocity.x);
        float yVelocity = Random.Range(MinCreationVelocity.y, MaxCreationVelocity.y);

        IsKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -FacingDir, yVelocity);

        Invoke("CancelKnockback", 1.5f);
    }

    private void cancelknockback() => IsKnocked = false;
}
