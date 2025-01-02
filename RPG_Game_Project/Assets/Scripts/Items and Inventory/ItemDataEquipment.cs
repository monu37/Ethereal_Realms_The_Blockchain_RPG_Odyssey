using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}


[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Ünique Effect")]
    public float ItemCoolDown;
    public ItemEffect[] effects;

    [Header("Major Stats")]
    public int Strength;
    public int Agility;
    public int Intelligence;
    public int Vitality;

    [Header("Offensive Stats")]
    public int Damage;
    public int CritChance;
    public int CritPower;

    [Header("Defansive Stats")]
    public int Health;
    public int Armor;
    public int Evasion;
    public int MagicalResistance;

    [Header("Magic Stats")]
    public int FireDamage;
    public int IceDamage;
    public int LightingDamage;

    [Header("Craft Requirement")]
    public List<InventoryItem> CraftingMaterials;

    int DescriptionLength;

    public void effect(Transform _enemyTransform)
    {
        foreach (var item in effects)
        {
            item.executeeffect(_enemyTransform);
        }
    }
    public void addmodifers()
    {
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        stats.Strength.addmodifier(Strength);
        stats.Agility.addmodifier(Agility);
        stats.Intelligence.addmodifier(Intelligence);
        stats.Vitality.addmodifier(Vitality);

        stats.Damage.addmodifier(Damage);
        stats.CritChance.addmodifier(CritChance);
        stats.CritPower.addmodifier(CritPower);

        stats.MaxHealth.addmodifier(Health);
        stats.Armor.addmodifier(Armor);
        stats.Evasion.addmodifier(Evasion);
        stats.MagicResistance.addmodifier(MagicalResistance);

        stats.FireDamage.addmodifier(FireDamage);
        stats.IceDamamge.addmodifier(IceDamage);
        stats.LightingDamage.addmodifier(LightingDamage);
    }

    public void removemodifers()
    {
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        stats.Strength.removemodifier(Strength);
        stats.Agility.removemodifier(Agility);
        stats.Intelligence.removemodifier(Intelligence);
        stats.Vitality.removemodifier(Vitality);

        stats.Damage.removemodifier(Damage);
        stats.CritChance.removemodifier(CritChance);
        stats.CritPower.removemodifier(CritPower);

        stats.MaxHealth.removemodifier(Health);
        stats.Armor.removemodifier(Armor);
        stats.Evasion.removemodifier(Evasion);
        stats.MagicResistance.removemodifier(MagicalResistance);

        stats.FireDamage.removemodifier(FireDamage);
        stats.IceDamamge.removemodifier(IceDamage);
        stats.LightingDamage.removemodifier(LightingDamage);
    }

    public override string GetDescription()
    {
        Sb.Length = 0;
        DescriptionLength = 0;

        additemdescription(Strength, "Strength");
        additemdescription(Agility, "Agility");
        additemdescription(Intelligence, "Intelligence");
        additemdescription(Vitality, "Vitality");

        additemdescription(Damage, "Damage");
        additemdescription(CritChance, "Crit.Chance");
        additemdescription(CritPower, "Crit.Power");


        additemdescription(Health, "Health");
        additemdescription(Evasion, "Evasion");
        additemdescription(Armor, "Armor");
        additemdescription(MagicalResistance, "Magic Resist.");

        additemdescription(FireDamage, "Fire Damage");
        additemdescription(IceDamage, "Ice Damage");
        additemdescription(LightingDamage, "Lighting Damage");


        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].EffectDescription.Length > 0)
            {
                Sb.AppendLine();
                Sb.AppendLine("Unique: " + effects[i].EffectDescription);

                DescriptionLength++;    
            }
        }


        if (DescriptionLength < 5)
        {
            for (int i = 0; i < 5 - DescriptionLength; i++)
            {
                Sb.AppendLine();
                Sb.Append("");
            }
        }

      
        return Sb.ToString();
    }

    public void additemdescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (Sb.Length > 0)
            {
                Sb.AppendLine();
            }
            if (_value > 0)
            {
                Sb.Append("+ " + _value + " " + _name);
            }

            DescriptionLength++;
        }
    }
}
