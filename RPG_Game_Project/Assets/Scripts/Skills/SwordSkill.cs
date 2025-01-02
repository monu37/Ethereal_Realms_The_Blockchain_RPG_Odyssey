using System;
using UnityEngine;
using UnityEngine.UI;


public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce Info")]
    [SerializeField] UISkilltreeSlot BounceUnlockButton;
    [SerializeField] int BounceAmount;
    [SerializeField] float BounceGravity;
    [SerializeField] float BounceSpped;

    [Header("Pierce Info")]
    [SerializeField] UISkilltreeSlot PierceUnlockButton;
    [SerializeField] int PierceAmount;
    [SerializeField]float PirceGravity;

    [Header("Skill Info")]
    [SerializeField] UISkilltreeSlot SwordUnlockButton;
    public bool IsSwordUnlocked; 
    [SerializeField] GameObject SwordPrefab;
    [SerializeField] Vector2 LaunchForce;
    [SerializeField] float SwordGravity;
    [SerializeField] float FreezeTimeDuration;
    [SerializeField] float ReturnSpeed;

    [Header("Spin Info")]
    [SerializeField] UISkilltreeSlot SpinUnlockButton;
    [SerializeField] float HitCoolDown = .35f;
    [SerializeField] float MaxTravelDistance = 7f;
    [SerializeField] float SpinDuration = 2;
    [SerializeField] float SpinGravity = 1;

    [Header("Passive Skills")]
    [SerializeField] UISkilltreeSlot TimeStopUnlockedButton;
    public bool IsTimeStopUnlocked;
    [SerializeField] UISkilltreeSlot VulnerableUnlockedButton;
    public bool IsVulnerableUnlocked;

    Vector2 FinalDir;

    [Header("Aim")]
    [SerializeField] GameObject DotPrefab;
    [SerializeField] Transform DotsParent;
    [SerializeField] int NumberOfDots;
    [SerializeField] float SpaceBetweenDots;
    GameObject[] AllDots;


    protected override void Start()
    {
        base.Start();

        generatedots();


        setupgravity();


        SwordUnlockButton.GetComponent<Button>().onClick.AddListener(unlocksword);
        BounceUnlockButton.GetComponent<Button>().onClick.AddListener(unlockbouncesword);
        PierceUnlockButton.GetComponent<Button>().onClick.AddListener(unlockpierce);
        SpinUnlockButton.GetComponent<Button>().onClick.AddListener(unlockspin);
        TimeStopUnlockedButton.GetComponent<Button>().onClick.AddListener(unlocktimestop);
        VulnerableUnlockedButton.GetComponent<Button>().onClick.AddListener(unlockvulnerable);
    }

    private void setupgravity()
    {
        if(swordType == SwordType.Bounce)
        {
            SwordGravity = BounceGravity;
        }
        else if(swordType == SwordType.Pierce)
        {
            SwordGravity = PirceGravity;
        } 
        else if(swordType == SwordType.Spin)
        {
            SwordGravity = SpinGravity;
        }
        
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            FinalDir = new Vector2(AimDir().normalized.x * LaunchForce.x, AimDir().normalized.y * LaunchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < AllDots.Length; i++)
            {
                AllDots[i].transform.position = DotPos(i * SpaceBetweenDots);
            }
        }

    }

    public void createsword()
    {
        GameObject newsword = Instantiate(SwordPrefab, player.transform.position, transform.rotation);
        swordSkillController newswordcontroller = newsword.GetComponent<swordSkillController>();


        if(swordType == SwordType.Bounce)
        {
            newswordcontroller.setupbounce(true, BounceAmount, BounceSpped);
        }
        else if(swordType == SwordType.Pierce)
        {
            newswordcontroller.setuppierce(PierceAmount);
        }
        else if(swordType == SwordType.Spin)
        {
            newswordcontroller.setupspin(true, MaxTravelDistance, SpinDuration,HitCoolDown);
        }


        newswordcontroller.setupsword(FinalDir, SwordGravity, player, FreezeTimeDuration,ReturnSpeed);

        player.assignnewsward(newsword);

        dotactive(false);
    }

    #region Unlock skills region


    protected override void checkunlock()
    {
        unlocksword();
        unlockbouncesword();
        unlockspin();
        unlockpierce();

        unlocktimestop();
        unlockvulnerable();

    }
    void unlocktimestop()
    {
        if (TimeStopUnlockedButton.IsUnlocked)
        {
            IsTimeStopUnlocked = true;
        }
    }
    void unlockvulnerable()
    {
        if(VulnerableUnlockedButton.IsUnlocked)
        {
            IsVulnerableUnlocked = true;
        }
    }
    void unlocksword()
    {
        if (SwordUnlockButton.IsUnlocked)
        {
            swordType= SwordType.Regular;
            IsSwordUnlocked = true;
        }
    }
    void unlockbouncesword()
    {
        if (BounceUnlockButton.IsUnlocked)
        {
            swordType= SwordType.Bounce;
        }
    }
    void unlockpierce()
    {
        if (PierceUnlockButton.IsUnlocked)
        {
            swordType = SwordType.Pierce;
        }
    }
    void unlockspin()
    {
        if (SpinUnlockButton.IsUnlocked)
        {
            swordType = SwordType.Spin;
        }
    }
    #endregion

    #region AimRegion
    public Vector2 AimDir()
    {
        Vector2 playerpos = player.transform.position;
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousepos - playerpos;

        return direction;
    }


    public void dotactive(bool _isactive)
    {
        for (int i = 0; i < NumberOfDots; i++)
        {
            AllDots[i].SetActive(_isactive);
        }
    }

    void generatedots()
    {
        AllDots = new GameObject[NumberOfDots];

        for (int i = 0; i < NumberOfDots; i++)
        {
            AllDots[i] = Instantiate(DotPrefab, player.transform.position, transform.rotation, DotsParent);
            AllDots[i].SetActive(false);
        }
    }

    Vector2 DotPos(float t)
    {
        Vector2 pos = (Vector2)player.transform.position + new Vector2(
            AimDir().normalized.x * LaunchForce.x,
            AimDir().normalized.y * LaunchForce.y) * t + .5f * (Physics2D.gravity * SwordGravity) * (t * t);

        return pos;
    }
    #endregion
}
