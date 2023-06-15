using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que define el estado de movimiento del jugador. Sus características son:
/// Hereda de PlayerGroundedState porque otros estados como PlayerIdleState permiten realizar
/// acciones similiares, por ejemplo usar habilidades o el ataque principal.
/// Define el movimiento del jugador en función de la entrada del usuario.
/// </summary>
public class PlayerMoveState : PlayerGroundedState {
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
