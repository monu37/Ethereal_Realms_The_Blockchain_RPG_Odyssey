using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBringerTriggers : EnemyAnimTrigger
{
    private EnemyDeathBringer enemyDeathBringer => GetComponentInParent<EnemyDeathBringer>();

    private void Relocate() => enemyDeathBringer.FindPosition();

    private void MakeInvisible() => enemyDeathBringer.Fx.maketransparent(true);
    private void MakeVisible() => enemyDeathBringer.Fx.maketransparent(false);
}
