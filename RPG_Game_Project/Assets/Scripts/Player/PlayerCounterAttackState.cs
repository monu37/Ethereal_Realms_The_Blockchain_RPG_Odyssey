using UnityEngine;

public class PlayerCounterAttackState : playerstate
{
    bool IsCanCreateClone;

    public PlayerCounterAttackState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsCanCreateClone = true;

        StateTimer = player.CounterAttackDuration;

        player.Anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.setzerovelocity();

        Collider2D[] collider = Physics2D.OverlapCircleAll(player.AttackCheckTrans.position, player.AttackCheckRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CheckCanBeStunned())
                {
                    StateTimer = 10; 
                    player.Anim.SetBool("SuccessfulCounterAttack", true);

                    player.SkillManager.Parry.useskill();//going to use to restore health on parry


                    if (IsCanCreateClone)
                    {
                      
                        IsCanCreateClone = false;
                        player.SkillManager.Parry.makemirageonparry(hit.transform);

                    }

                }
            }
        }

        if (StateTimer < 0 || IsTriggerCalled)
        {
            StateMachine.ChangeState(player.IdleState);
        }
    }
}
