using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    Player player;

    Animator Anim;
    SpriteRenderer Sr;
    [SerializeField] float ColorLoosingSpeed;

    float CloneTimer;
    float AttackMultiplier;
    [SerializeField] Transform AttackCheckTrans;
    [SerializeField] float AttackCheckRadius = .8f;
    Transform ClosestEnemy;
    int FacingDir = 1;


    float ChanceToDuplicate;
    bool IsCanDuplicateClone;

    private void Awake()
    {
        Sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CloneTimer -= Time.deltaTime;
        if (CloneTimer < 0)
        {
            Sr.color = new Color(1, 1, 1, Sr.color.a - (Time.deltaTime * ColorLoosingSpeed));

            if (Sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void setupclone(Transform _newtransform, float _clonedur, bool _iscanattack, Vector3 _offset,
        Transform _closestenemy, bool _iscanduplicateclone, float _chancetoduplicate, Player _player, float _attackmultiplier)
    {
        if (_iscanattack)
        {
            Anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        transform.position = _newtransform.position + _offset;

        CloneTimer = _clonedur;
        ClosestEnemy = _closestenemy;
        IsCanDuplicateClone = _iscanduplicateclone;
        ChanceToDuplicate = _chancetoduplicate;
        player = _player;
        AttackMultiplier = _attackmultiplier;

        faceclosesttraget();
    }

    void animationtrigger()
    {
        CloneTimer = -1f;
    }

    void attacktrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(AttackCheckTrans.position, AttackCheckRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Entity>().setupknockbackdirection(transform);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemystats = hit.GetComponent<EnemyStats>();

                playerStats.clonedodamage(enemystats, AttackMultiplier);

                if(player.SkillManager.Clone.IsCanAppltOnHitEffect)
                {
                    ItemDataEquipment weapondata = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

                    if (weapondata != null)
                    {
                        weapondata.effect(hit.transform);
                    }


                }
                //player.Stats.dodamage(hit.GetComponent<CharacterStats>());


                if (IsCanDuplicateClone)
                {
                    if (Random.Range(0, 100) < 35)
                    {
                        SkillManager.instance.Clone.createclone(hit.transform, new Vector3(.5f * FacingDir, 0));
                    }
                }
            }


        }
    }

    void faceclosesttraget()
    {

        if (ClosestEnemy != null)
        {
            if (transform.position.x > ClosestEnemy.position.x)
            {
                FacingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
