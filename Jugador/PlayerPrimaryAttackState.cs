using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState {

    public int ComboCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        xInput = 0;  // we need this to fix bug on attack direction

        if (ComboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            ComboCounter = 0;

        player.anim.SetInteger("ComboCounter", ComboCounter);


        float attackDir = player.facingDir;

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
