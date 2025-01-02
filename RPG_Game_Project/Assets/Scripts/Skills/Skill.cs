using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float DefaultCoolDown;
    public float CoolDownTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;

        checkunlock();
    }
    protected virtual void Update()
    {
        CoolDownTimer -= Time.deltaTime; 
    }

    protected virtual void checkunlock()
    {

    }

    public virtual bool IsCanUseSkill()
    {
        if(CoolDownTimer < 0)
        {
            useskill();

            CoolDownTimer = DefaultCoolDown;
            return true;
        }

        player.Fx.createpopuptext("CoolDown");
        print("Skill is on cooldown ");

        return false;
    }

    public virtual void useskill()
    {
        // do some specific skill things
    }

    protected virtual Transform FindClosestEnemy(Transform _checktransform)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_checktransform.position, 25);

        float ClosestDistance = float.PositiveInfinity;
        Transform ClosestEnemy = null;

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float DistanceToEnemy = Vector2.Distance(_checktransform.position, hit.transform.position);

                if (DistanceToEnemy < ClosestDistance)
                {
                    ClosestDistance = DistanceToEnemy;
                    ClosestEnemy = hit.transform;
                }
            }
        }

        return ClosestEnemy;
    }
}
