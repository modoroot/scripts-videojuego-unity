using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que define la acción de atrapar la espada, una de las habilidades del jugador. Sus características son:
/// Se comprueba si es necesario voltear el personaje por si la espada está a su lado contrario.
/// Se bloquean las acciones que puede realizar el jugador durante un corto periodo de tiempo.
/// </summary>
public class PlayerCatchSwordState : PlayerState {
    // Transform que representa la espada
    private Transform sword;
    // Constructor que recibe el jugador, la máquina de estados y el nombre de la animación
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        // Se obtiene eltransform de la espada
        sword = player.Sword.transform;
        // Se verifica la posición del jugador y se voltea si es necesario
        if (player.transform.position.x > sword.position.x && player.FacingDir == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.FacingDir == -1)
            player.Flip();

        // Se establece la velocidad del jugador para que regrese la espada
        rb.velocity = new Vector2(player.swordReturnImpact * -player.FacingDir, rb.velocity.y);
    }

    public override void Exit() {
        base.Exit();

        // Se inicia una corrutina para que el jugador no pueda realizar acciones durante un corto periodo de tiempo
        player.StartCoroutine("BusyFor", .1f);
    }

    public override void Update() {
        base.Update();

        // Si se llama al trigger, se cambia al estado Idle
        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }

}
