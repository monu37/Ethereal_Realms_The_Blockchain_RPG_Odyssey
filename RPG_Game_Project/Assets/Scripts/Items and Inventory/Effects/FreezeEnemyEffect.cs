using UnityEngine;


[CreateAssetMenu(fileName = "Freeze Enemies Effect", menuName = "Data/Item Effect/Freeze Enemies Effect")]
public class FreezeEnemyEffect : ItemEffect
{
    [SerializeField] float Duration;

    public override void executeeffect(Transform _transform)
    {
        PlayerStats stat = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (stat.CurrentHealth > stat.GetMaxHealthValue() * .1f)
        {
            return;
        }

        if (!Inventory.Instance.IsCanUseArmor())
        {
            return;
        }

        Collider2D[] collider = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in collider)
        {
            hit.GetComponent<Enemy>()?.freezetimefor(Duration);
        }
    }

}
