using UnityEngine;
using UnityEngine.UI;

public class DodgeSkill : Skill
{
    [Header("Dodge")]
    [SerializeField] UISkilltreeSlot UnlockDodgeButton;
    [SerializeField] int EvasionAmount; //dodgeamount
    public bool IsDodgeUnlocked;

    [Header("Mirage dodge")]
    [SerializeField] UISkilltreeSlot UnlockMirageDodge;
    public bool IsMirageDodgeUnlocked;

    protected override void Start()
    {
        base.Start();


        //
        UnlockDodgeButton.GetComponent<Button>().onClick.AddListener(unlockdodge);
        UnlockMirageDodge.GetComponent<Button>().onClick.AddListener(unlockmiragedodge);
    }

    protected override void checkunlock()
    {
        unlockdodge();
        unlockmiragedodge();
    }

    void unlockdodge()
    {
        if (UnlockDodgeButton.IsUnlocked  &&!IsDodgeUnlocked)
        {
            print("Player dodge the attack");
            player.Stats.Evasion.addmodifier(EvasionAmount);
            Inventory.Instance.updatestatsui();

            IsDodgeUnlocked = true;
        }
    }

    void unlockmiragedodge()
    {
        if (UnlockDodgeButton.IsUnlocked )
        {
            IsMirageDodgeUnlocked= true;
        }
    }

    public void createmirageondodge()
    {
        if(IsMirageDodgeUnlocked)
        {
            print("Player create clone from dodge");
            SkillManager.instance.Clone.createclone(player.transform, new Vector3(2 * player.FacingDir, 0));
        }
    }
}
