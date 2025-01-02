using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackholeSkill : Skill
{
    [SerializeField] UISkilltreeSlot BlackHoleUnlockButton;
    public bool IsBlackHoleUnlocked;

    [SerializeField] GameObject BlackholePrefab;
    [SerializeField] float MaxSize;
    [SerializeField] float GrowSpeed;
    [SerializeField] float ShrinkSpeed;

    [Space]
    [SerializeField] int AmountOfAttack;
    [SerializeField] float CloneAttackCoolDown;
    [SerializeField] float BlackholeDuration;


    BlackholeSkillController CurrentBlackholeScript;

    void unlockblackhole()
    {
        if (BlackHoleUnlockButton.IsUnlocked)
        {
            IsBlackHoleUnlocked = true;
        }
    }

    public override bool IsCanUseSkill()
    {
        return base.IsCanUseSkill();
    }

    public override void useskill()
    {
        base.useskill();

        GameObject newblackhole = Instantiate(BlackholePrefab, player.transform.position, Quaternion.identity);

        CurrentBlackholeScript = newblackhole.GetComponent<BlackholeSkillController>();

        CurrentBlackholeScript.setupblackhole(MaxSize, GrowSpeed, ShrinkSpeed, AmountOfAttack, CloneAttackCoolDown,BlackholeDuration);

        AudioManager.instance.playsfx(3,player.transform);
        AudioManager.instance.playsfx(4,player.transform);
    }

    protected override void Start()
    {
        base.Start();

        BlackHoleUnlockButton.GetComponent<Button>().onClick.AddListener(unlockblackhole);
    }

    protected override void checkunlock()
    {

        unlockblackhole();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool IsSkillCompleted()
    {

        if (!CurrentBlackholeScript)
        {
            return false;
        }

        if (CurrentBlackholeScript.IsPlayerCanExitState)
        {
            CurrentBlackholeScript = null;
            return true;
        }

        return false;
    }

    public float GetBlackholeRadius()
    {
        return MaxSize / 2;
    }
}
