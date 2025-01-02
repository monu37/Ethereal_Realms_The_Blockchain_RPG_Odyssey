using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy enemy;
    ItemDrop MyDropSystem;
    public Stats CurrencyDropAmount;

    [Header("Level Info")]
    [SerializeField] int Level = 1;

    [Range(0, 1f)]
    [SerializeField] float PercentageModifer = .4f;


    protected override void Start()
    {
        CurrencyDropAmount.setdefaultvalue(100);
        applylevelmodifiers();

        base.Start();


        enemy = GetComponent<Enemy>();

        MyDropSystem = GetComponent<ItemDrop>();
    }

    private void applylevelmodifiers()
    {
        //modify(Strength);
        //modify(Agility);
        //modify(Intelligence);
        //modify(Vitality);

        modify(Damage);
        modify(CritChance);
        modify(CritPower);

        modify(MaxHealth);
        modify(Armor);
        modify(Evasion);
        modify(MagicResistance);

        modify(FireDamage);
        modify(IceDamamge);
        modify(LightingDamage);

        modify(CurrencyDropAmount);


    }

    void modify(Stats _stat)
    {
        for (int i = 1; i < Level; i++)
        {
            float modifer = _stat.GetValue() * PercentageModifer;

            _stat.addmodifier(Mathf.RoundToInt(modifer));
        }
    }

    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);

        //enemy.damageeffect();
    }

    public override void die()
    {
        base.die();

        enemy.die();

        if (!IsFall)
        {
            PlayerManager.instance.Currency += CurrencyDropAmount.GetValue();
            MyDropSystem.generatedrop();
        }
      
        //LevelManager.instance.SkeletonDied++;

        Destroy(gameObject, 2f);
    }
}
