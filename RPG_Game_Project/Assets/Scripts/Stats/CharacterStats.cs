using System.Collections;
using UnityEngine;

public enum StatType
{
    Strength,
    Agility,
    Intelligence,
    Vitality,
    Damage,
    CritChance,
    CritPower,
    Health,
    Armor,
    Evasion,
    MagicRes,
    FireDamage,
    IceDamage,
    LightingDamage
}

public class CharacterStats : MonoBehaviour
{
    EntityFx Fx;


    public int CurrentHealth;
    [Space]

    [Header("Major Stats")]
    public Stats Strength;  //1 point increase damage by1 and critical power by 1%
    public Stats Agility;  // 1 point increase evasion by 1% and critical chance by 1%
    public Stats Intelligence;  // 1 point increase magic damage by 1 and magic resistance by 3
    public Stats Vitality;  // 1 point increase health by 3 or 5 points

    [Header("Offensive Stats")]
    public Stats Damage;
    public Stats CritChance;
    public Stats CritPower;     // default value 150%



    [Header("Defansive Stats")]
    public Stats MaxHealth;
    public Stats Armor;
    public Stats Evasion;
    public Stats MagicResistance;

    [Header("Magic Stats")]
    public Stats FireDamage;
    public Stats IceDamamge;
    public Stats LightingDamage;


    [Space]
    [SerializeField] float AilmentDuration = 4;
    public bool IsIgnited;    //does damage over time
    public bool IsChilled;    //reduce armor by 20%
    public bool IsShocked;    // reduce accuracy by 20%

    float IgnitedTimer;
    float ChilledTimer;
    float ShockedTimer;


    float IgniteDamageCoolDown = .3f;
    float IgniteDamageTimer;

    int IgniteDamage;
    [SerializeField] GameObject ShockStrikePrefab;
    int ShockedDamage;

    public System.Action OnHealthChange;

    public bool IsDead { get; private set; }
    public bool IsInvincible {  get; private set; }
    bool IsVulnerable;

    public bool IsFall;

    protected virtual void Start()
    {

        CritPower.setdefaultvalue(150);


        CurrentHealth = GetMaxHealthValue();

        Fx = GetComponent<EntityFx>();


        print("Char stats Called");
    }

    protected virtual void Update()
    {
        IgnitedTimer -= Time.deltaTime;
        ChilledTimer -= Time.deltaTime;
        ShockedTimer -= Time.deltaTime;

        IgniteDamageTimer -= Time.deltaTime;

        if (IgnitedTimer < 0)
        {
            IsIgnited = false;
        }

        if (ChilledTimer < 0)
        {
            IsChilled = false;
        }

        if (ShockedTimer < 0)
        {
            IsShocked = false;
        }


        if (IsIgnited)
        {

            applyignitedamage();
        }
    }

    public void makevulnerablefor(float _duration)
    {
        print("Vulnerable time : " + _duration);
        StartCoroutine(vulnerableforcoroutine(_duration));
    }

    IEnumerator vulnerableforcoroutine(float _duration)
    {
        IsVulnerable = true;
        print("Is vulnerbale : " + IsVulnerable);
        yield return new WaitForSeconds(_duration);

        IsVulnerable = false;
        print("Is vulnerbale : " + IsVulnerable);
    }

    public virtual void increasestatsby(int _modifier, float _duration, Stats _stattomodify)
    {
        StartCoroutine(statmodifierCoroutine(_modifier, _duration, _stattomodify));
    }

    IEnumerator statmodifierCoroutine(int _modifier, float _duration, Stats _stattomodify)
    {
        _stattomodify.addmodifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _stattomodify.removemodifier(_modifier);
    }

    public virtual void dodamage(CharacterStats _targetstats)
    {

        bool IscriticalStrike = false;

        if (_targetstats.IsInvincible)
            return;

        if (IsTargetCanAvoidAttack(_targetstats))
        {
            return;
        }

        _targetstats.GetComponent<Entity>().setupknockbackdirection(transform);

        int totaldamage = Damage.GetValue() + Strength.GetValue();

        if (IsCanCrit())
        {
            totaldamage = CalculateCriticalDamage(totaldamage);
            IscriticalStrike = true;
        }


        Fx.createhitfx(_targetstats.transform, IscriticalStrike);


        totaldamage = checktargetarmor(_targetstats, totaldamage);
        _targetstats.takedamage(totaldamage);


        domagicaldamage(_targetstats);

    }


    #region Magical damage and ailment
    public virtual void domagicaldamage(CharacterStats _targetstats)
    {

        int _FireDamage = FireDamage.GetValue();
        int _IceDamage = IceDamamge.GetValue();
        int _LightingDamage = LightingDamage.GetValue();

        int totalmagicdamage = _FireDamage + _IceDamage + _LightingDamage + Intelligence.GetValue();

        totalmagicdamage = checktargetresistance(_targetstats, totalmagicdamage);

        _targetstats.takedamage(totalmagicdamage);



        //
        if (Mathf.Max(_FireDamage, _IceDamage, _LightingDamage) <= 0)
        {
            return;
        }

        attempttoapplyailment(_targetstats, _FireDamage, _IceDamage, _LightingDamage);
    }

    private void attempttoapplyailment(CharacterStats _targetstats, int _FireDamage, int _IceDamage, int _LightingDamage)
    {
        bool IsCanApplyIgnite = _FireDamage > _IceDamage && _FireDamage > _LightingDamage;
        bool IsCanApplyChill = _IceDamage > _FireDamage && _IceDamage > _LightingDamage;
        bool IsCanApplyShock = _LightingDamage > _FireDamage && _LightingDamage > _IceDamage;


        while (!IsCanApplyIgnite && !IsCanApplyChill && !IsCanApplyShock)
        {
            if (Random.value < .5f && _FireDamage > 0)
            {
                IsCanApplyIgnite = true;
                _targetstats.applyailment(IsCanApplyIgnite, IsCanApplyChill, IsCanApplyShock);
                print("Apply fire");
                return;

            }

            if (Random.value < .5f && _IceDamage > 0)
            {
                IsCanApplyChill = true;
                _targetstats.applyailment(IsCanApplyIgnite, IsCanApplyChill, IsCanApplyShock);
                print("Apply ice");
                return;

            }

            if (Random.value < .5f && _LightingDamage > 0)
            {
                IsCanApplyShock = true;
                _targetstats.applyailment(IsCanApplyIgnite, IsCanApplyChill, IsCanApplyShock);
                print("Apply lighting");
                return;

            }
        }


        if (IsCanApplyIgnite)
        {
            _targetstats.setupignitedamage(Mathf.RoundToInt(_FireDamage * .2f));
        }

        if (IsCanApplyShock)
        {
            _targetstats.setupshockstrikedamage(Mathf.RoundToInt(_LightingDamage * .1f));
        }

        _targetstats.applyailment(IsCanApplyIgnite, IsCanApplyChill, IsCanApplyShock);
    }



    public void applyailment(bool _ignite, bool _chill, bool _shock)
    {
        bool IsCanApplyIgnite = !IsIgnited && !IsChilled && !IsShocked;
        bool IsCanApplyChill = !IsIgnited && !IsChilled && !IsShocked;
        bool IsCanApplyShock = !IsIgnited && !IsChilled;


        if (_ignite && IsCanApplyIgnite)
        {
            IsIgnited = _ignite;
            IgnitedTimer = AilmentDuration;

            Fx.ignitefxfor(AilmentDuration);

        }
        if (_chill && IsCanApplyChill)
        {
            ChilledTimer = AilmentDuration;
            IsChilled = _chill;

            float slowpercentage = .2f;
            GetComponent<Entity>().slowentityby(slowpercentage, AilmentDuration);
            Fx.chillfxfor(AilmentDuration);

        }
        if (_shock && IsCanApplyShock)
        {
            if (IsShocked)
            {
                applyshock(_shock);

            }
            else
            {
                //find closest target , only among the enemies
                // Instantiate thunder strike
                //setup thunder strike


                if (GetComponent<Player>() != null)
                {
                    return;
                }

                hitnearesttargetwithshockstrike();

            }




        }

    }

    public void applyshock(bool _shock)
    {
        if (IsShocked) return;

        ShockedTimer = AilmentDuration;
        IsShocked = _shock;

        Fx.shockfxfor(AilmentDuration);
    }

    private void hitnearesttargetwithshockstrike()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 25);

        float ClosestDistance = float.PositiveInfinity;
        Transform ClosestEnemy = null;

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float DistanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (DistanceToEnemy < ClosestDistance)
                {
                    ClosestDistance = DistanceToEnemy;
                    ClosestEnemy = hit.transform;
                }
            }
            if (ClosestEnemy == null)
            {
                ClosestEnemy = transform;
            }
        }

        if (ClosestEnemy != null)
        {
            GameObject newshockstrike = Instantiate(ShockStrikePrefab, transform.position, Quaternion.identity);

            newshockstrike.GetComponent<ShockStrikeController>().damage(ShockedDamage, ClosestEnemy.GetComponent<CharacterStats>());
        }
    }

    public void setupignitedamage(int _damage) => IgniteDamage = _damage;
    public void setupshockstrikedamage(int _damage) => ShockedDamage = _damage;
    private void applyignitedamage()
    {
        if (IgniteDamageTimer < 0)
        {
            print("Take Burn Damage " + IgniteDamage);

            decreasehealthby(IgniteDamage);

            if (CurrentHealth < 0 && !IsDead)
            {
                die();
            }

            IgniteDamageTimer = IgniteDamageCoolDown;
        }
    }

    #endregion
    public virtual void takedamage(int _damage)
    {
        if(IsInvincible)
        {
            return;
        }

        decreasehealthby(_damage);

        GetComponent<Entity>().damageimpact();

        Fx.StartCoroutine("flashfx");



        if (CurrentHealth <= 0 && !IsDead)
        {
            die();
        }


    }


    public virtual void increasehealthby(int _Amount)
    {

        CurrentHealth += _Amount;

        if (CurrentHealth > GetMaxHealthValue())
        {
            CurrentHealth = GetMaxHealthValue();
        }

        if (OnHealthChange != null)
        {
            OnHealthChange();
        }
    }
    protected virtual void decreasehealthby(int _damage)
    {
        
        if (IsVulnerable)
        {
            
            _damage = Mathf.RoundToInt(_damage * 1.5f);
        }

        CurrentHealth -= _damage;

        if(_damage > 0)
        {
            Fx.createpopuptext(_damage.ToString());
        }

        if (OnHealthChange != null)
        {
            OnHealthChange();
        }
    }

    public virtual void die()
    {
        print(gameObject.name + " Died");
        IsDead = true;
    }

    public void killentity()
    {
        if(!IsDead)
        {
            die();
        }
    }

    public void makeinvincible(bool _isinvincible) => IsInvincible = _isinvincible;

    #region Stats Calculation
    protected int checktargetarmor(CharacterStats _targetstats, int totaldamage)
    {

        if (_targetstats.IsChilled)
        {
            totaldamage -= Mathf.RoundToInt(_targetstats.Armor.GetValue() * .8f);
        }
        else
        {
            totaldamage -= _targetstats.Armor.GetValue();
        }


        totaldamage -= _targetstats.Armor.GetValue();

        totaldamage = Mathf.Clamp(totaldamage, 0, int.MaxValue);
        return totaldamage;
    }

    private int checktargetresistance(CharacterStats _targetstats, int totalmagicdamage)
    {
        totalmagicdamage -= _targetstats.MagicResistance.GetValue() + (_targetstats.Intelligence.GetValue() * 3);
        totalmagicdamage = Mathf.Clamp(totalmagicdamage, 0, int.MaxValue);
        return totalmagicdamage;
    }

    public virtual void onevasion()//dodge
    {

    }

    protected bool IsTargetCanAvoidAttack(CharacterStats _targetstats)
    {
        int TotalEvasion = _targetstats.Evasion.GetValue() + _targetstats.Agility.GetValue();

        if (IsShocked)
        {
            TotalEvasion += 20;
        }

        if (Random.Range(0, 100) < TotalEvasion)
        {
            print("Attack avoided");
            _targetstats.onevasion();
            return true;
        }

        return false;
    }

    protected bool IsCanCrit()
    {
        int TotalCriticalChance = CritChance.GetValue() + Agility.GetValue();


        if (Random.Range(0, 100) <= TotalCriticalChance)
        {
            return true;
        }

        return false;
    }

   protected int CalculateCriticalDamage(int _damge)
    {
        float TotalCritPower = (CritPower.GetValue() + Strength.GetValue()) * .01f;

        float CritDamage = _damge * TotalCritPower;

        return Mathf.RoundToInt(CritDamage);
    }

    public int GetMaxHealthValue()
    {
        return MaxHealth.GetValue() + Vitality.GetValue() * 5;
    }

    #endregion

    public Stats GetStat(StatType _statType)
    {
        if (_statType == StatType.Strength) { return Strength; }
        else if (_statType == StatType.Agility) { return Agility; }
        else if (_statType == StatType.Intelligence) { return Intelligence; }
        else if (_statType == StatType.Vitality) { return Vitality; }

        else if (_statType == StatType.Damage) { return Damage; }
        else if (_statType == StatType.CritChance) { return CritChance; }
        else if (_statType == StatType.CritPower) { return CritPower; }

        else if (_statType == StatType.Health) { return MaxHealth; }
        else if (_statType == StatType.Armor) { return Armor; }
        else if (_statType == StatType.Evasion) { return Evasion; }
        else if (_statType == StatType.MagicRes) { return MagicResistance; }


        else if (_statType == StatType.FireDamage) { return FireDamage; }
        else if (_statType == StatType.IceDamage) { return IceDamamge; }
        else if (_statType == StatType.LightingDamage) { return LightingDamage; }



        return null;
    }











}
