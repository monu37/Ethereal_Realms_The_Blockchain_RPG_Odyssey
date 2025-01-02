using UnityEngine;

public class ThunderStrikeController : MonoBehaviour
{
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            PlayerStats PStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

            EnemyStats target = col.GetComponent<EnemyStats>();

            PStats.domagicaldamage(target);

        }
    }
}
