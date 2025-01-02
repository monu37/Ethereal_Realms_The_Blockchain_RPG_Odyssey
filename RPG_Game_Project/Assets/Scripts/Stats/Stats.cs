using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    [SerializeField] int BaseValue;

    public List<int> Modifiers;

    public int GetValue()
    {
        int finalvalue = BaseValue;

        foreach (int i in Modifiers) 
        {
            finalvalue += i;    
        }

        return finalvalue;
    }

    public void setdefaultvalue(int _value)
    {
        BaseValue = _value;
    }

    public void addmodifier(int _modifier)
    {
        Modifiers.Add(_modifier);
    }

    public void removemodifier(int _modifier)
    {
        Modifiers.Remove(_modifier);

    }
}
