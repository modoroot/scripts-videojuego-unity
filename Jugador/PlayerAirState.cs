using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que define el estado en el aire del jugador. Sus características son:
/// Si detecta un muro a partir del WallCheck, cambia al estado WallSlide.
/// Si detecta suelo, cambia al estado Idle.
/// Si se presiona Space, se ajusta la velocidad del jugador del eje Y y del eje X.
/// </summary>
public class PlayerAirState : PlayerState {
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();


        if (player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlide);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}
