using UnityEngine;

public class PlayerStats : CharacterStats
{
    Player player;

    protected override void Start()
    {
        base.Start();


        player = GetComponent<Player>();
    }

    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);

        //player.damageeffect();
    }

    public override void die()
    {
        base.die();

        player.die();

        GameManager.instance.LostCurrencyAmount = PlayerManager.instance.Currency;

        PlayerManager.instance.Currency = 0;

        GetComponent<PlayerItemDrop>()?.generatedrop();
    }

    protected override void decreasehealthby(int _damage)
    {
        base.decreasehealthby(_damage);

        if (_damage > GetMaxHealthValue() * .3f)
        {
            player.setupknockbackpower(new Vector2(10, 6));
            player.Fx.screenshake(player.Fx.ShakeHighDamage);
            print("High damage taken");

            AudioManager.instance.playsfx(Random.Range(34, 35), null);
        }

        ItemDataEquipment currentarmor = Inventory.Instance.GetEquipment(EquipmentType.Armor);

        if (currentarmor != null)
        {
            currentarmor.effect(player.transform);
        }
    }

    public override void onevasion()
    {

        player.SkillManager.Dodge.createmirageondodge();
    }

    public void clonedodamage(CharacterStats _targetstats, float _attackmultipiler)
    {
        if (IsTargetCanAvoidAttack(_targetstats))
        {
            return;
        }



        int totaldamage = Damage.GetValue() + Strength.GetValue();

        if (_attackmultipiler > 0)
        {
            totaldamage = Mathf.RoundToInt(_attackmultipiler);
        }

        if (IsCanCrit())
        {
            totaldamage = CalculateCriticalDamage(totaldamage);
        }





        totaldamage = checktargetarmor(_targetstats, totaldamage);

        _targetstats.takedamage(totaldamage);


        domagicaldamage(_targetstats);
    }
}
