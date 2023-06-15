using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que controla el salto desde una pared del jugador. Sus caracter�sticas son:
/// Salta en la direcci�n opuesta a la que est� mirando.
/// Cambia de estado a IdleState si toca el suelo y a AirState si est� en el aire.
/// </summary>
public class PlayerWallJumpState : PlayerState {
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        stateTimer = 1f;
        player.SetVelocity(5 * -player.FacingDir, player.jumpForce);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.AirState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
