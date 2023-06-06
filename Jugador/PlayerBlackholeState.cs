using UnityEngine;

/// <summary>
/// Clase que controla los estados del jugador cuando usa la habilidad
/// </summary>
public class PlayerBlackholeState : PlayerState {
    private float flyTime = .4f;
    private bool skillUsed;


    private float defaultGravity;
    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void Enter() {
        base.Enter();

        defaultGravity = player.rb.gravityScale;

        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit() {
        base.Exit();

        player.rb.gravityScale = defaultGravity;
        player.fx.MakeTransprent(false);
    }

    public override void Update() {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);

        if (stateTimer < 0) {
            rb.velocity = new Vector2(0, -.1f);

            if (!skillUsed) {
                if (player.Skill.Blackhole.CanUseSkill())
                    skillUsed = true;
            }
        }

        if (player.Skill.Blackhole.SkillCompleted())
            stateMachine.ChangeState(player.AirState);
    }
}
