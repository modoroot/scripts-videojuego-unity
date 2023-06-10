using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueGroundedState : EnemyState {
    protected Transform player;
    protected Enemy_Rogue enemy;
    public RogueGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Rogue _enemy) : base(_enemyBase, _stateMachine, _animBoolName) {
        enemy = _enemy;
    }
    public override void Enter() {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 5)
            stateMachine.ChangeState(enemy.BattleState);
    }
}
