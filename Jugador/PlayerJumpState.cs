using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que define el estado de salto del jugador. Sus caracter�sticas son:
/// Salta cuando se pulsa la tecla Space seg�n la fuerza de salto definida en el jugador.
/// Si la velocidad en el eje Y es menor que 0, cambia al estado de ca�da.
/// </summary>
public class PlayerJumpState : PlayerState {
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.AirState);
    }
}
