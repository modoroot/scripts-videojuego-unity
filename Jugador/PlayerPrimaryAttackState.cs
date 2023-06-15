using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Clase que define el estado de ataque principal del jugador. Sus características son:
/// Contiene un contador de combos que se incrementa cada vez que se llama a este estado.
/// Hace un ataque diferente dependiendo del valor del contador de combos.
/// </summary>
public class PlayerPrimaryAttackState : PlayerState {

    public int ComboCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        xInput = 0;

        if (ComboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            ComboCounter = 0;

        player.anim.SetInteger("ComboCounter", ComboCounter);


        float attackDir = player.FacingDir;

        if (xInput != 0)
            attackDir = xInput;


        player.SetVelocity(player.attackMovement[ComboCounter].x * attackDir, player.attackMovement[ComboCounter].y);


        stateTimer = .1f;
    }

    public override void Exit() {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);

        ComboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update() {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }


}
