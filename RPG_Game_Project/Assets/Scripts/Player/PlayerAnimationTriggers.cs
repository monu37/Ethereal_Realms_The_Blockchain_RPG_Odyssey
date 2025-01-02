using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();
    void animationtrigger()
    {
        player.animationtrigger();
    }

    void attacktrigger()
    {
        AudioManager.instance.playsfx(2,null);

        Collider2D[] collider = Physics2D.OverlapCircleAll(player.AttackCheckTrans.position, player.AttackCheckRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {

                EnemyStats target = hit.GetComponent<EnemyStats>();

                if (target != null)
                {
                    player.Stats.dodamage(target);

                }


                //inventory get weapon call item effect

                ItemDataEquipment weapondata = Inventory.Instance.GetEquipment(EquipmentType.Weapon);

                if (weapondata != null)
                {
                    weapondata.effect(target.transform);
                }




            }
        }
    }


    void throwsword()
    {
        SkillManager.instance.Sword.createsword();
    }
}
