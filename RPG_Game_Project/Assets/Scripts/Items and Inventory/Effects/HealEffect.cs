using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]
public class HealEffect : ItemEffect
{
    [Range(0,1)]
    [SerializeField] float HealPercentage;
    public override void executeeffect(Transform _enemyTransform)
    {
        //player stats
        PlayerStats _playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        //how much to heal
        int healamount = Mathf.RoundToInt(_playerStats.GetMaxHealthValue() * HealPercentage);


        // heal
        _playerStats.increasehealthby(healamount);

    }
}
