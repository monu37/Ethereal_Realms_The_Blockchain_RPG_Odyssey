using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class swordSkillController : MonoBehaviour
{
    Animator Anim;
    Rigidbody2D Rb;
    CircleCollider2D Cd;
    Player player;

    bool IsCanRotate = true;

    float ReturnSpeed = 12f;
    bool IsReturning;


    float FreezeTimeDuration;

    [Header("Bounce Info")]
    float BounceSpeed;
    bool IsBouncing;
    int BounceAmount;
    List<Transform> EnemyTargetTrans;
    int TargetIndex;


    [Header("Pierce Info")]
    int PierceAmount;

    [Header("Spin Info")]
    float MaxTravelDistance;
    float SPinDuration;
    float SpinTImer;
    bool IsSpinStopped;
    bool IsSpinning;

    float HitTimer;
    float HitCooldown;

    float SpinDirection;

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Cd = GetComponent<CircleCollider2D>();
    }

    void destroy()
    {
        Destroy(gameObject);
    }

    public void setupsword(Vector2 _dir, float _gravityscale, Player _player, float _freezetimeduration, float _returnspeed)
    {

        player = _player;
        Rb.velocity = _dir;
        Rb.gravityScale = _gravityscale;
        FreezeTimeDuration = _freezetimeduration;
        ReturnSpeed = _returnspeed;

        if (PierceAmount <= 0)
        {
            Anim.SetBool("Rotation", true);
        }

        SpinDirection = math.clamp(Rb.velocity.x, -1, 1);

        Invoke(nameof(destroy), 7);

    }

    public void setupbounce(bool _isbounce, int _amountofbounces, float _bouncespeed)
    {
        IsBouncing = _isbounce;
        BounceAmount = _amountofbounces;
        BounceSpeed = _bouncespeed;

        EnemyTargetTrans = new List<Transform>();
    }

    public void setuppierce(int _pierceamount)
    {
        PierceAmount = _pierceamount;

    }

    public void setupspin(bool _isspin, float _maxtraveldistance, float _spinduration, float _hitcooldown)
    {
        IsSpinning = _isspin;
        MaxTravelDistance = _maxtraveldistance;
        SPinDuration = _spinduration;
        HitCooldown = _hitcooldown;
    }

    public void returnsword()
    {
        Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //Rb.isKinematic = false;
        transform.parent = null;
        IsReturning = true;



    }

    private void Update()
    {//
        if (IsCanRotate)
        {

            transform.right = Rb.velocity;
        }

        //
        if (IsReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, ReturnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1f)
            {
                player.catchthesword();
            }
        }

        bouncelogic();

        spinlogic();
    }

    private void spinlogic()
    {
        if (IsSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > MaxTravelDistance && !IsSpinStopped)
            {
                stopspinning();

            }

            if (IsSpinStopped)
            {
                SpinTImer -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(transform.position.x + SpinDirection, transform.position.y), 1.5f * Time.deltaTime);

                if (SpinTImer < 0)
                {
                    IsReturning = true;
                    IsSpinning = false;
                }


                HitTimer -= Time.deltaTime;
                if (HitTimer < 0)
                {
                    HitTimer = HitCooldown;

                    Collider2D[] cds = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in cds)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                        {
                            swordskilldamage(hit.GetComponent<Enemy>());
                        }
                    }
                }
            }
        }
    }

    private void stopspinning()
    {
        IsSpinStopped = true;
        Rb.constraints = RigidbodyConstraints2D.FreezePosition;
        SpinTImer = SPinDuration;
    }

    private void bouncelogic()
    {
        if (IsBouncing && EnemyTargetTrans.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, EnemyTargetTrans[TargetIndex].position,
                BounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, EnemyTargetTrans[TargetIndex].position) < .1f)
            {
                swordskilldamage(EnemyTargetTrans[TargetIndex].GetComponent<Enemy>());


                TargetIndex++;
                BounceAmount--;
                if (BounceAmount < 0)
                {
                    IsBouncing = false;
                    IsReturning = true;
                }

                if (TargetIndex >= EnemyTargetTrans.Count)
                {
                    TargetIndex = 0;

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsReturning)
        {
            return;
        }

        if (col.GetComponent<Enemy>() != null)
        {
            Enemy enemy = col.GetComponent<Enemy>();


            swordskilldamage(enemy);
        }



        //for bouncing
        setuptargetforbounce(col);

        stuckthesword(col);
    }

    private void swordskilldamage(Enemy _enemy)
    {
        EnemyStats enemyStats = _enemy.GetComponent<EnemyStats>();

        if (player.SkillManager.Sword.IsTimeStopUnlocked)//time stop
        {
            _enemy.freezetimefor(FreezeTimeDuration);

        }


        if (player.SkillManager.Sword.IsVulnerableUnlocked)
        {
            print("Make it vulnerable");
            enemyStats.makevulnerablefor(FreezeTimeDuration);
        }


        player.Stats.dodamage(enemyStats);
        ItemDataEquipment equipmentamulet = Inventory.Instance.GetEquipment(EquipmentType.Amulet);

        if (equipmentamulet != null)
        {
            equipmentamulet.effect(_enemy.transform);
        }
    }

    private void setuptargetforbounce(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            if (IsBouncing && EnemyTargetTrans.Count <= 0)
            {
                Collider2D[] cds = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in cds)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        EnemyTargetTrans.Add(hit.transform);
                    }
                }
            }
        }
    }

    private void stuckthesword(Collider2D col)
    {
        if (PierceAmount > 0 && col.GetComponent<Enemy>() != null)
        {
            PierceAmount--;
            return;
        }

        if (IsSpinning)
        {
            stopspinning();
            return;
        }


        IsCanRotate = false;
        Cd.enabled = false;

        Rb.isKinematic = true;
        Rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (IsBouncing && EnemyTargetTrans.Count > 0)
        {
            return;
        }

        Anim.SetBool("Rotation", false);
        transform.parent = col.transform;
    }
}
