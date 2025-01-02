using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public SpriteRenderer Sr { get; private set; }

    public CharacterStats Stats { get; private set; }
    public CapsuleCollider2D Cd { get; private set; }

    #endregion

    [Header("KnockBack Info")]
    [SerializeField] protected Vector2 KnockBackPower;
    [SerializeField] protected float KnockBackDuration;
    protected bool IsKnocked;


    [Header("Collision Info")]
    public Transform AttackCheckTrans;
    public float AttackCheckRadius;
    [SerializeField] protected Transform GroundCheckTrans;
    [SerializeField] protected float GroundCheckDistance;
    [SerializeField] protected Transform WallCheckTrans;
    [SerializeField] protected float WallCheckDistance;
    [SerializeField] protected LayerMask GroundLayerMask;

    public int KnockBackDirection { get; private set; }
    public int FacingDir { get; private set; } = 1;
    protected bool IsFacingRight = true;


    public System.Action OnFlipped;


    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Sr = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<Animator>();

        Rb = GetComponent<Rigidbody2D>();
       

        Stats = GetComponentInChildren<CharacterStats>();
        Cd = GetComponent<CapsuleCollider2D>();

    }


    public virtual void slowentityby(float _slowpercentage, float _slowduration)
    {

    }

    protected virtual void returndefaultspped()
    {
        Anim.speed = 1;
    }

    public virtual void damageimpact()
    {
        StartCoroutine(hitknockback());
    }

    public virtual void setupknockbackdirection(Transform _damagedirection)
    {
        if (_damagedirection.position.x > transform.position.x)
        {
            KnockBackDirection = 1;
        }
        else if (_damagedirection.position.x < transform.position.x)
        {
            KnockBackDirection = -1;
        }
    }

    protected virtual void Update()
    {

    }

    public void setupknockbackpower(Vector2 _power) => KnockBackPower = _power; 

    protected virtual IEnumerator hitknockback()
    {
        IsKnocked = true;
        Rb.velocity = new Vector2(KnockBackPower.x * -KnockBackDirection, KnockBackPower.y);

        yield return new WaitForSeconds(KnockBackDuration);

        IsKnocked = false;
        setupzeroknockbackpower();
    }

    protected virtual void setupzeroknockbackpower()
    {

    }

    #region velocity
    public void setzerovelocity()
    {
        if (IsKnocked)
        {
            return;
        }

        Rb.velocity = Vector2.zero;
    }

    public void setvelocity(float x_velocity, float y_velocity)
    {
        if (IsKnocked)
        {
            return;
        }

        Rb.velocity = new Vector2(x_velocity, y_velocity);


        flipcontroller(x_velocity);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(GroundCheckTrans.position, Vector2.down, GroundCheckDistance, GroundLayerMask);

    public virtual bool IsWallDetected() => Physics2D.Raycast(WallCheckTrans.position, Vector2.right * FacingDir, WallCheckDistance, GroundLayerMask);



    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(GroundCheckTrans.position, new Vector3(GroundCheckTrans.position.x, GroundCheckTrans.position.y - GroundCheckDistance));
        Gizmos.DrawLine(WallCheckTrans.position, new Vector3(WallCheckTrans.position.x + WallCheckDistance*FacingDir, WallCheckTrans.position.y));
        Gizmos.DrawWireSphere(AttackCheckTrans.position, AttackCheckRadius);
    }

    #endregion

    #region Flip
    public virtual void flip()
    {
        FacingDir *= -1;

        IsFacingRight = !IsFacingRight;

        transform.Rotate(0, 180f, 0);

        if (OnFlipped != null)
        {
            OnFlipped();

        }
    }

    public void flipcontroller(float x)
    {
        if (x > 0 && !IsFacingRight)
        {
            flip();
        }
        else if (x < 0 && IsFacingRight)
        {
            flip();
        }
    }
    public virtual void setupdefailtfacingdir(int _direction)
    {
        FacingDir = _direction;

        if (FacingDir == -1)
            IsFacingRight = false;
    }
    #endregion



    public virtual void die()
    {

    }

}
