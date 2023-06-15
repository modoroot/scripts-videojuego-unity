using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que define el estado de idle del jugador. Sus características son:
/// Velocidad en X e Y = 0
/// Detecta si hay un muro a su lado y si es así, no se mueve
/// Comprueba si se pulsa una tecla de movimiento y si el jugador no está realizando
/// otra acción y si es así, cambia al estado de movimiento
/// </summary>
public class PlayerIdleState : PlayerGroundedState {
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        player.SetZeroVelocity();

    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (xInput == player.FacingDir && player.IsWallDetected())
            return;

        if (xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.MoveState);
    }
}
