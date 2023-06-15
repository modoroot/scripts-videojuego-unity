using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que controla la muerte del jugador (dentro del juego). Sus características son:
/// Oscurece la pantalla usando el prefab de DarkScreen cuando el jugador muere (dentro del juego).
/// Ajusta la velocidad del jugador a 0.
/// Todas las animaciones del jugador se paran.
/// </summary>
public class PlayerDeadState : PlayerState {
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void Enter() {
        base.Enter();
        GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        player.SetZeroVelocity();
    }
}
