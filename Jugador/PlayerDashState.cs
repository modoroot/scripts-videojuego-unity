using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState {
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
        player.Skill.Dash.CloneOnDash();
        stateTimer = player.dashDuration;
        player.stats.MakeInvulnerable(true);
    }

    public override void Exit() {
        base.Exit();

        player.Skill.Dash.CloneOnArrival();
        player.SetVelocity(0, rb.velocity.y);
        player.stats.MakeInvulnerable(false);
    }

    public override void Update() {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlide);

        player.SetVelocity(player.dashSpeed * player.DashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.IdleState);


    }
}
