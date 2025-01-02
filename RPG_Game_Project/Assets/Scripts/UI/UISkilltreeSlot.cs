using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkilltreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    UI ui;

    [SerializeField] int SkillPrice;
    [SerializeField] string SkillName;
    [TextArea]
    [SerializeField] string SkillDescription;
    [SerializeField] Color LockedSkillColor;


    public bool IsUnlocked;
    [SerializeField] UISkilltreeSlot[] Unlocked;
    [SerializeField] UISkilltreeSlot[] Locked;

    Image SkillImage;


    private void OnValidate()
    {
        gameObject.name = "Skill Tree Slot: " + SkillName;

      


    }

    private void Awake()
    {
        
        GetComponent<Button>().onClick.AddListener(unlockskillslot);
    }

    private void Start()
    {
        SkillImage = GetComponent<Image>();
        ui=GetComponentInParent<UI>();

        SkillImage.color = LockedSkillColor;
       
        if (IsUnlocked)
        {
            SkillImage.color = Color.white;
        }
       
    }

 
   

    public void unlockskillslot()
    {
        print("Unlock skill button");

        if (!PlayerManager.instance.IsHaveEnoughCurrency(SkillPrice))
        {
            return;
        }

       
        for (int i = 0; i < Unlocked.Length; i++)
        {
            if (!Unlocked[i].IsUnlocked)
            {
                print("Can not unlock skill");
                return;
            }


        }

        for (int i = 0; i < Locked.Length; i++)
        {
            if (Locked[i].IsUnlocked)
            {
                print("Can not unlock skill");
                return;
            }

        }

        IsUnlocked = true;
        SkillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.SkillToolTip.showtooltip(SkillDescription, SkillName, SkillPrice);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.SkillToolTip.hidetooltip();
    }

    public void loaddata(GameData _data)
    {
        Debug.Log("Load SKill tree data");
        if(_data.SKillTree.TryGetValue(SkillName, out  bool value))
        {
            IsUnlocked = value;
        }
    }

    public void savedata(ref GameData _data)
    {
        print("save SKill tree");
        if (_data.SKillTree.TryGetValue(SkillName, out bool value))
        {
            _data.SKillTree.Remove(SkillName);
            _data.SKillTree.Add(SkillName, IsUnlocked);
        }
        else
        {
            _data.SKillTree.Add(SkillName, IsUnlocked);

        }
    }
}
