using UnityEngine;

public class PlayerBlackholeState : playerstate
{
    float FlyTime = .4f;
    bool IsSkillUsed;

    float DefaultGravity;
    public PlayerBlackholeState(Player _player, playerstatemachine _statemachine, string _animBoolName) : base(_player, _statemachine, _animBoolName)
    {
    }

    public override void animationfinishtrigger()
    {
        base.animationfinishtrigger();
    }

    public override void Enter()
    {
        base.Enter();

        DefaultGravity = Rb.gravityScale;

        IsSkillUsed = false;
        StateTimer = FlyTime;
        Rb.gravityScale = 0;

    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit");

        Rb.gravityScale = DefaultGravity;
        PlayerManager.instance.player.Fx.maketransparent(false);


    }

    public override void Update()
    {
        base.Update();

        if (StateTimer > 0)
        {
            Rb.velocity = new Vector2(0, 15);
        }

        if (StateTimer < 0)
        {
            Rb.velocity = new Vector2(0, -.1f);

            if (!IsSkillUsed)
            {
                if (player.SkillManager.Blackhole.IsCanUseSkill())
                {

                    IsSkillUsed = true;
                }

            }
        }


        if (player.SkillManager.Blackhole.IsSkillCompleted())
        {
            StateMachine.ChangeState(player.AirState);
        }
    }
}
