using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    [Header("Dash")]
    [SerializeField] UISkilltreeSlot DashUnlockedButton;
    public bool IsDashUnlocked { get; private set; }

    [Header("CLone on dash")]
    [SerializeField] UISkilltreeSlot CloneOnDashUnlockedButton;
    public bool IsCloneOnDashUnlocked { get; private set; }

    [Header("Clone on arrival")]
    [SerializeField] UISkilltreeSlot CloneOnArrivalUnlockedButton;
    public bool IsCloneOnArrivalUnlocked { get; private set; }

    public override void useskill()
    {
        base.useskill();

    }

    protected override void Start()
    {
        base.Start();

        DashUnlockedButton.GetComponent<Button>().onClick.AddListener(() => unlockdash());
        CloneOnDashUnlockedButton.GetComponent<Button>().onClick.AddListener(() => unlockcloneondash());
        CloneOnArrivalUnlockedButton.GetComponent<Button>().onClick.AddListener(() => unlockcloneonarrival());
    }

    protected override void checkunlock()
    {
        unlockdash();
        unlockcloneondash();
        unlockcloneonarrival();
    }

    void unlockdash()
    {
        //DashUnlockedButton.unlockskillslot();
        
        if (DashUnlockedButton.IsUnlocked)
        {
            print("Unlock dash");
            IsDashUnlocked = true;

        }
    }

    void unlockcloneondash()
    {
        //CloneOnDashUnlockedButton.unlockskillslot();

        if (CloneOnDashUnlockedButton.IsUnlocked)
        {
            print("Unlock clone on start");
            IsCloneOnDashUnlocked = true;

        }
    }

    void unlockcloneonarrival()
    {
        //CloneOnArrivalUnlockedButton.unlockskillslot();

        if (CloneOnArrivalUnlockedButton.IsUnlocked)
        {
            print("Unlock clone on end/Arrival");

            IsCloneOnArrivalUnlocked = true;
        }

    }

    public void cloneondash() // create clone on dash
    {
       
        if (CloneOnDashUnlockedButton.IsUnlocked)
        {
            print("Clone created on start of dash");
           SkillManager.instance.Clone.createclone(player.transform, Vector3.zero);
        }
    }

    public void cloneonarrival() // create clone on arrival
    {
        if (CloneOnArrivalUnlockedButton.IsUnlocked)
        {
            print("Clone created on end of dash");
            SkillManager.instance.Clone.createclone(player.transform, Vector3.zero);
        }
    }
}
