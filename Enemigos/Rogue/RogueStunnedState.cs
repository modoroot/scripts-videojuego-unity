using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueStunnedState : EnemyState {
    private Enemy_Rogue enemy;
    public RogueStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Rogue enemy) : base(_enemyBase, _stateMachine, _animBoolName) {
        this.enemy = enemy;
    }

    public override void Enter() {
        base.Enter();

        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);

        stateTimer = enemy.stunDuration;

        rb.velocity = new Vector2(-enemy.FacingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit() {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update() {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.IdleState);
    }
}
