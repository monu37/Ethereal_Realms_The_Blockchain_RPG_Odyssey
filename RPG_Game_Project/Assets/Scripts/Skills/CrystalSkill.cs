using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalSkill : Skill
{
    [SerializeField] GameObject CrystalPrefab;
    GameObject CurrentCrystal;
    [SerializeField] float CrystalDuration;

    [Header("Simple Crystal")]
    [SerializeField] UISkilltreeSlot UnlockCrystalButton;
    public bool IsCrystalUnlocked;

    [Header("Explosive Crystal")]
    [SerializeField] UISkilltreeSlot UnlockExplosiveCrystalButton;
    [SerializeField] bool IsCanExplode;


    [Header("Moving Crystal")]
    [SerializeField] UISkilltreeSlot UnlockMovingCrystalButton;
    [SerializeField] bool IsCanMoveToEnemy;
    [SerializeField] float MoveSpeed;

    [Header("Multi Stacking Crystal")]
    [SerializeField] UISkilltreeSlot UnlockMultiStackButton;
    [SerializeField] bool IsCanUseMultiStack;
    [SerializeField] int AmountOfStacks;
    [SerializeField] float MultiStackCooldown;
    [SerializeField] float UseTimeWindow;
    [SerializeField] List<GameObject> CrystalsLeft = new List<GameObject>();

    [Header("Mirage Crystal")]
    [SerializeField] UISkilltreeSlot UnlockCloneInsteadOfCrystalButton;
    [SerializeField] bool IsCloneInsteadOfCrystal;


    protected override void Start()
    {
        base.Start();

        UnlockCrystalButton.GetComponent<Button>().onClick.AddListener(unlockcrystal);
        UnlockExplosiveCrystalButton.GetComponent<Button>().onClick.AddListener(unlockexplosivecrystal);
        UnlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(unlockmovingcrystal);
        UnlockMultiStackButton.GetComponent<Button>().onClick.AddListener(unlockmultistackcrystal);
        UnlockCloneInsteadOfCrystalButton.GetComponent<Button>().onClick.AddListener(unlockcloneinsteadofcrystal);
    }


    protected override void checkunlock()
    {
        unlockcrystal();
        unlockexplosivecrystal();
        unlockmovingcrystal();
        unlockmultistackcrystal();
        unlockcloneinsteadofcrystal();

    }

    void unlockcrystal()
    {
        if(UnlockCrystalButton.IsUnlocked)
        {
            IsCrystalUnlocked = true;
        }
    }
    void unlockexplosivecrystal()
    {
        if (UnlockExplosiveCrystalButton.IsUnlocked)
        {
            IsCanExplode = true;
        }
    }
    void unlockmovingcrystal()
    {
        if(UnlockMovingCrystalButton.IsUnlocked)
        {
            IsCanMoveToEnemy = true;
        }
    }
    void unlockmultistackcrystal()
    {
        if(UnlockMultiStackButton.IsUnlocked)
        {
            IsCanUseMultiStack = true;
        }

    }

    void unlockcloneinsteadofcrystal()
    {
        if (UnlockCloneInsteadOfCrystalButton.IsUnlocked)
        {
            IsCloneInsteadOfCrystal = true;
        }
    }






    public override void useskill()
    {
        base.useskill();

        if (IsCheckUseMultiCrystal())
        {
            return;
        }


        if (CurrentCrystal == null)
        {
            createcrystal();
        }
        else
        {
            if (IsCanMoveToEnemy) return;

            Vector2 playerpos = player.transform.position;
            player.transform.position = CurrentCrystal.transform.position;
            CurrentCrystal.transform.position = playerpos;


            if (IsCloneInsteadOfCrystal)
            {
                SkillManager.instance.Clone.createclone(CurrentCrystal.transform, Vector3.zero);
                Destroy(CurrentCrystal);
            }
            else
            {
                CurrentCrystal.GetComponent<CrystalSkillController>()?.finishcrystal();

            }


        }


    }

    public void createcrystal()
    {
        CurrentCrystal = Instantiate(CrystalPrefab, player.transform.position, Quaternion.identity);

        CrystalSkillController currentcrystalscript = CurrentCrystal.GetComponent<CrystalSkillController>();
        currentcrystalscript.setupcrsytal(CrystalDuration, IsCanExplode, IsCanMoveToEnemy, MoveSpeed,
            FindClosestEnemy(CurrentCrystal.transform),player);

    }

    public void currentcrystalchooserandomenemy()
    {
        CurrentCrystal.GetComponent<CrystalSkillController>().chooserandomenemy();
    }

    bool IsCheckUseMultiCrystal()
    {

        if (IsCanUseMultiStack)
        {

            if (CrystalsLeft.Count > 0)
            {

                if (CrystalsLeft.Count == AmountOfStacks)
                {
                    Invoke(nameof(resetability), UseTimeWindow);
                }

                DefaultCoolDown = 0;

                GameObject crystaltospawn = CrystalsLeft[CrystalsLeft.Count - 1];
                GameObject newcrsytal = Instantiate(crystaltospawn, player.transform.position, Quaternion.identity);


                CrystalsLeft.Remove(crystaltospawn);

                newcrsytal.GetComponent<CrystalSkillController>().
                    setupcrsytal(CrystalDuration, IsCanExplode, IsCanMoveToEnemy, MoveSpeed, FindClosestEnemy(newcrsytal.transform), player);


                if (CrystalsLeft.Count <= 0)
                {
                    print("Refil time");
                    DefaultCoolDown = MultiStackCooldown;
                    refilcrystal();
                }
            }

            return true;
        }
        return false;
    }

    void refilcrystal()
    {
        int totalsatck = AmountOfStacks - CrystalsLeft.Count;

        for (int i = 0; i < totalsatck; i++)
        {
            CrystalsLeft.Add(CrystalPrefab);
        }
    }

    public void resetability()
    {

        if (DefaultCoolDown > 0)
        {
            return;
        }

        DefaultCoolDown = MultiStackCooldown;
        refilcrystal();
    }
}
