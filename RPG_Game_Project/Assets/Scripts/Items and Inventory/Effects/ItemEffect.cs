
using UnityEngine;



public class ItemEffect : ScriptableObject
{

    [TextArea]
    public string EffectDescription;
    public virtual void executeeffect(Transform _enemyTransform)
    {
        Debug.Log("Effect Execute");
    }
}
