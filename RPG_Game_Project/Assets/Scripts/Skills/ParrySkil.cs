using UnityEngine;
using UnityEngine.UI;

public class ParrySkil : Skill
{
    [Header("Parry")]
    [SerializeField] UISkilltreeSlot ParryUnlockButton;
    public bool IsParryUnlocked { get; private set; }

    [Header("Parry restore")]
    [SerializeField] UISkilltreeSlot ParryRestoreUnlockButton;
    [Range(0f,1)]
    [SerializeField]float RestoreHealthAmount;
    public bool IsParryRestoreUnlocked { get; private set; }

    [Header("Parry with mirage")]
    [SerializeField] UISkilltreeSlot ParryWithMirageUnlockButton;
    public bool IsParryWithMirageUnlocked { get; private set; }

    public override void useskill()
    {
        base.useskill();

        if(IsParryRestoreUnlocked)
        {
            int restoreamount = Mathf.RoundToInt(player.Stats.GetMaxHealthValue() * RestoreHealthAmount);
            player.Stats.increasehealthby(restoreamount);
        }
    }

    protected override void Start()
    {
        base.Start();

        ParryUnlockButton.GetComponent<Button>().onClick.AddListener(unlockparry);
        ParryRestoreUnlockButton.GetComponent<Button>().onClick.AddListener(unlockparryrestore);
        ParryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(unlockparrywithmirage);
    }

    protected override void checkunlock()
    {
        unlockparry();
        unlockparryrestore();
        unlockparrywithmirage();
    }

    void unlockparry()
    {
        if (ParryUnlockButton.IsUnlocked)
        {
            IsParryUnlocked = true;
        }
    }

    void unlockparryrestore()
    {
        if (ParryRestoreUnlockButton.IsUnlocked)
        {
            IsParryRestoreUnlocked = true;
        }
    }

    void unlockparrywithmirage()
    {
        if (ParryWithMirageUnlockButton.IsUnlocked)
        {
            IsParryWithMirageUnlocked = true;
        }
    }

    public void makemirageonparry(Transform _respawntransform)
    {
        if (IsParryWithMirageUnlocked)
        {
            SkillManager.instance.Clone.createclonewithdelay(_respawntransform);
        }
    }
}
