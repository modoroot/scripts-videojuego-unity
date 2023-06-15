using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Estado que apunta con la espada, una de las habilidades del jugador. Sus características son:
/// Al momento de apuntar, se crean unos puntos en pantalla con la trayectoria de la espada.
/// El jugador entra en estado ocupado para que no pueda moverse mientras apunta.
/// Se ajusta su velocidad a 0 para que no se mueva.
/// Se cambia el sentido del personaje dependiendo de la posición del mouse.
/// </summary>
public class PlayerAimSwordState : PlayerState {
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        player.Skill.Sword.DotsActive(true);
    }

    public override void Exit() {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update() {
        base.Update();

        player.SetZeroVelocity();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.IdleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.FacingDir == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.FacingDir == -1)
            player.Flip();
    }
}
