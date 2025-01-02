using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimTrigger : MonoBehaviour
{
    Enemy enemy=> GetComponentInParent<Enemy>();

    void animationtrigger() => enemy.animationfinishtrigger();

    void attacktrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(enemy.AttackCheckTrans.position, enemy.AttackCheckRadius);

        foreach(var hit in collider)
        {
            if(hit.GetComponent<Player>() != null)
            {

                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.Stats.dodamage(target);

            }
        }
    }
    private void speicalattacktrigger()
    {
        enemy.animationspecialattacktrigger();
    }

    void opencounterwindow()=>enemy.opencounterattackwindow();

     void closecounterwindow() =>enemy.closecounterattackwindow();
}
