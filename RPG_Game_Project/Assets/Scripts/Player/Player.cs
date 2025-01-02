using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    [Header("Attack Details")]
    public Vector2[] AttackMovement;
    public float CounterAttackDuration = .2f;

    public bool IsBusy {  get; private set; }

    [Header("Move Info")]
    public float MoveSpeed = 12f;
    public float JumpForce = 12f;
    public float SwordReturnForce;
    float DefaultMoveSpeed;
     float DefaultJumpForce;

    [Header("Dash Info")]
    public float DashSpeed;
    public float DashDuration;
    float DefaultDashSpeed;

    public float DashDir {  get; private set; }
    

   public SkillManager SkillManager { get; private set; }
    public GameObject Sword {  get; private set; }

    #region State
    public playerstatemachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }  
    public PlayerMoveState MoveState { get; private set;}
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }    
    public PlayerWallSlideState wallSlideState { get; private set; }    
    public PlayerWallJumpState WallJumpState { get; private set; }//
    public PlayerDashState DashState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    public PlayerCounterAttackState CounterAttackState { get; private set; }

    public PlayerAimSwordState AimSwordState { get; private set; }
    public PlayerCatchSwordState CatchState { get; private set; }
    public PlayerBlackholeState BlackholeState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }


    #endregion

    public PlayerFx Fx { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        StateMachine = new playerstatemachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        CounterAttackState = new PlayerCounterAttackState(this, StateMachine, "CounterAttack");


        AimSwordState = new PlayerAimSwordState(this, StateMachine, "AimSword");
        CatchState = new PlayerCatchSwordState(this, StateMachine, "CatchSword");


        BlackholeState = new PlayerBlackholeState(this, StateMachine, "Jump");
        DeadState = new PlayerDeadState(this, StateMachine, "Die");

    


    }

    protected override void Start()
    {
        base.Start();

        Fx = GetComponent<PlayerFx>();

        SkillManager = SkillManager.instance;

        StateMachine.Initialize(IdleState);


        DefaultJumpForce = JumpForce;
        DefaultMoveSpeed = MoveSpeed;
        DefaultDashSpeed = DashSpeed;

    }


    protected override void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        base .Update();

        StateMachine.CurrentState.Update();

        checkdashinpiut();

        if(Input.GetKeyDown(KeyCode.F) && SkillManager.Crystal.IsCrystalUnlocked) //using crystal
        {
            SkillManager.Crystal.IsCanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("flask");
            Inventory.Instance.useflask();
        }

       
    }

    public override void slowentityby(float _slowpercentage, float _slowduration)
    {
        MoveSpeed = MoveSpeed * (1 - _slowpercentage);

        JumpForce = JumpForce * (1 - _slowpercentage);

        DashSpeed = DashSpeed * (1 - _slowpercentage);

        Anim.speed=Anim.speed*(1-_slowpercentage);

        Invoke(nameof(returndefaultspped), _slowduration);

    }

    protected override void returndefaultspped()
    {
        base.returndefaultspped();

        MoveSpeed = DefaultMoveSpeed;
        JumpForce =DefaultJumpForce;
        DashSpeed = DefaultDashSpeed;
    }

    public void assignnewsward(GameObject _newsword)
    {
        Sword = _newsword;
    }

    public void catchthesword()
    {
        StateMachine.ChangeState(CatchState);
        Destroy(Sword);
    }

    
    public IEnumerator busyfor(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;

    }


    public void checkdashinpiut()
    {

        if (IsWallDetected())
        {
            return;
        }

        if (SkillManager.Dash.IsDashUnlocked ==false)
        {
            return;
        }

        //DashCoolDownTimer -= Time.deltaTime;//

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.Dash.IsCanUseSkill())
        {

            //DashCoolDownTimer = DashCoolDownDefaultTime;
            DashDir = Input.GetAxisRaw("Horizontal");

            if(DashDir == 0)
            {
                DashDir = FacingDir;
            }

            StateMachine.ChangeState(DashState);
        }
    }

   

   

   
    public void animationtrigger() => StateMachine.CurrentState.animationfinishtrigger();


    public override void die()
    {
        base.die();

        StateMachine.ChangeState(DeadState);
    }

    protected override void setupzeroknockbackpower()
    {
        KnockBackPower = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ParticleSystem>() != null && collision.gameObject.name == "Fire")
        {
            print("vv");
        }

      
    }
}
