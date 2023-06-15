using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que controla el dash del jugador. Sus características son:
/// Controla la velocidad a la que se desliza el jugador
/// Controla si el jugador está en el aire y toca un muro. Si lo toca, se agarra y pasa
/// al estado PlayerWallSlideState. Si no, pasa a AirState o IdleState,
/// dependiendo de si está en el aire o no.
/// </summary>
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
