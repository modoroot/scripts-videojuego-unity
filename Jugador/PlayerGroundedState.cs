using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado general del jugador cuando está en el suelo. Sus características son:
/// Permite al jugador utilizar todas sus habilidades, ataques y movimientos.
/// </summary>
public class PlayerGroundedState : PlayerState {
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R) && player.Skill.Blackhole.blackholeUnlocked) {
            stateMachine.ChangeState(player.BlackHole);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.Skill.Sword.swordUnlocked)
            stateMachine.ChangeState(player.AimSword);

        if (Input.GetKeyDown(KeyCode.Q) && player.Skill.Parry.parryUnlocked)
            stateMachine.ChangeState(player.CounterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.PrimaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.AirState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
    }

    private bool HasNoSword() {
        if (!player.Sword) {
            return true;
        }

        player.Sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
