using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{

    Animator Anim;
    CircleCollider2D Cd;

    float CrystalTimer;
    bool IsCanExplode;
    bool IsCanMove;
    float MoveSpeed;

    [Space]
    bool IsCanGrow;
    float GrowSpeed = 5;

    Transform ClosestTarget;
    [SerializeField] LayerMask EnemyLayerMask;

    Player player;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Cd = GetComponent<CircleCollider2D>();
    }

    public void setupcrsytal(float _duartion, bool _canexplode, bool _canmove, float _movespeed, Transform _closesttarget,Player _player)
    {
        CrystalTimer = _duartion;
        IsCanExplode = _canexplode;
        IsCanMove = _canmove;
        MoveSpeed = _movespeed;
        ClosestTarget = _closesttarget;
        player = _player;
    }

    public void chooserandomenemy()
    {
        float radius=SkillManager.instance.Blackhole.GetBlackholeRadius();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, EnemyLayerMask);

        if (colliders.Length > 0)
        {
            ClosestTarget = colliders[Random.Range(0, colliders.Length)].transform;

        }



    }

    private void Update()
    {

        CrystalTimer -= Time.deltaTime;

        if (CrystalTimer < 0)
        {
            finishcrystal();

        }

        if (IsCanMove)
        {

            if(ClosestTarget == null)
            {
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, ClosestTarget.position, MoveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, ClosestTarget.position) < 1)
            {
                finishcrystal();
                IsCanMove = false;
            }
        }

        if (IsCanGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector3(3, 3), GrowSpeed * Time.deltaTime);
        }
    }

    public void finishcrystal()
    {
        //crystal explode
        if (IsCanExplode)
        {
            IsCanGrow = true;
            Anim.SetTrigger("Explode");
            //animationexplodedevent();
        }
        else
        {
            selfdestroy();
        }
    }

    public void selfdestroy()
    {
        Destroy(gameObject);
    }

    void animationexplodedevent()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, Cd.radius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Entity>().setupknockbackdirection(transform);

                print(hit.gameObject.name);
                player.Stats.domagicaldamage(hit.GetComponent<CharacterStats>());

                ItemDataEquipment equipmentamulet = Inventory.Instance.GetEquipment(EquipmentType.Amulet);

                if (equipmentamulet != null)
                {
                    equipmentamulet.effect(hit.transform);
                }
            }
        }
    }
}
