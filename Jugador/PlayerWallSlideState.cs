using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que controla el deslizamiento del jugador por una pared. Sus características son:
/// Controla la velocidad a la que se desliza el jugador.
/// Cambia de estado a PlayerWallJumpState si se pulsa la tecla de salto.
/// </summary>
public class PlayerWallSlideState : PlayerState {
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (player.IsWallDetected() == false)
            stateMachine.ChangeState(player.AirState);

        if (Input.GetKeyDown(KeyCode.Space)) {
            stateMachine.ChangeState(player.WallJump);
            return;
        }

        if (xInput != 0 && player.FacingDir != xInput)
            stateMachine.ChangeState(player.IdleState);

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);

    }

}
