using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloneSkill : Skill
{

    [Header("Clone Info")]
    [SerializeField] float AttackMultiplier;
    [SerializeField] GameObject ClonePrefab;
    [SerializeField] float CloneDuration;

    [Space]

    [Header("Clone Attack")]
    [SerializeField] UISkilltreeSlot CloneAttackUnlockButton;
    [SerializeField] float CloneAttackMultiplier;
    public bool IsCloneAttackUnlocked;
    [SerializeField] bool IsCanAttack;

    [Header("Aggressive Clone")]
    [SerializeField] UISkilltreeSlot AggressiveCloneUnlockButton;
    [SerializeField]float AggressiveCloneAttackMultiplier;
    public bool IsCanAppltOnHitEffect;

    [Header("Multiple Clone")]
    [SerializeField] UISkilltreeSlot MultiplierUnlockButton;
    [SerializeField]float MultiplierCloneAttackMultiplier;
    [SerializeField] bool IsCanDuplicateClone;
    [SerializeField] float ChanceToDuplicate;

    [Space]
    [Header("Crystal Instead Of Clone")]
    [SerializeField] UISkilltreeSlot CrystalInsteadOfCloneUnlockButton;
    public bool IsCrystalInsteadOfClone;


    protected override void Start()
    {
        base.Start();

        CloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(unlockcloneattack);
        AggressiveCloneUnlockButton.GetComponent<Button>().onClick.AddListener(unlockaggressiveclone);
        MultiplierUnlockButton.GetComponent<Button>().onClick.AddListener(unlockmulticlone);
        CrystalInsteadOfCloneUnlockButton.GetComponent<Button>().onClick.AddListener(unlockcrystalinsteadofclone);
    }

    #region Unlock skills
    protected override void checkunlock()
    {
        unlockcloneattack();
        unlockaggressiveclone();
        unlockmulticlone();
        unlockcrystalinsteadofclone();
    }


    void unlockcloneattack()
    {
        if (CloneAttackUnlockButton.IsUnlocked)
        {
            IsCanAttack = true;
            AttackMultiplier = CloneAttackMultiplier;
        }
    }

    void unlockaggressiveclone()
    {
        if (AggressiveCloneUnlockButton.IsUnlocked)
        {
            IsCanAppltOnHitEffect = true;
            AttackMultiplier = AggressiveCloneAttackMultiplier;
        }
    }
    void unlockmulticlone()
    {
        if (MultiplierUnlockButton.IsUnlocked)
        {
            IsCanDuplicateClone = true;
            AttackMultiplier = MultiplierCloneAttackMultiplier;
        }
    }
    void unlockcrystalinsteadofclone()
    {
        if (CrystalInsteadOfCloneUnlockButton.IsUnlocked)
        {
            IsCrystalInsteadOfClone = true;
        }
    }

    #endregion



    public void createclone(Transform _clonepos, Vector3 _offset)
    {
        if(IsCrystalInsteadOfClone)
        {
            SkillManager.instance.Crystal.createcrystal();

            return;
        }

        GameObject newclone = Instantiate(ClonePrefab);
        newclone.GetComponent<CloneSkillController>().setupclone(_clonepos, CloneDuration, IsCanAttack, _offset,
            FindClosestEnemy(newclone.transform), IsCanDuplicateClone, ChanceToDuplicate,player,AttackMultiplier);
    }

   

    public void createclonewithdelay(Transform _enemytrans)
    {
        StartCoroutine(clonedelaycoroutine(_enemytrans, new Vector3(1.5f * player.FacingDir, 0, 0)));
    }

    IEnumerator clonedelaycoroutine(Transform _enemytrans, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);

        createclone(_enemytrans.transform, _offset);
    }
}
