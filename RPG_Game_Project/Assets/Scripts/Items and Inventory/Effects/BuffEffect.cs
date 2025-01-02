using UnityEngine;




[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
public class BuffEffect : ItemEffect
{


    PlayerStats stats;
    [SerializeField] StatType BuffType;
    [SerializeField] int BuffAmount;
    [SerializeField] float BuffDuration;


    public override void executeeffect(Transform _enemyTransform)
    {
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        stats.increasestatsby(BuffAmount, BuffDuration, stats.GetStat(BuffType));
    }

    
}
