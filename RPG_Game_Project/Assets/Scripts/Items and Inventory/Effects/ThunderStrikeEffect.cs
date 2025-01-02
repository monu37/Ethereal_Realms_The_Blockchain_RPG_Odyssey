using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Item Effect/Thunder Strike")]
public class ThunderStrikeEffect : ItemEffect
{

    [SerializeField] GameObject ThunderStrikePrefab;

    public override void executeeffect(Transform _enemyTransform)
    {
        GameObject newthunderstrike = Instantiate(ThunderStrikePrefab, _enemyTransform.position, Quaternion.identity);

        Destroy(newthunderstrike, 1f);
    }
}
