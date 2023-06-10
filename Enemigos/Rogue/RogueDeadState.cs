using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueDeadState : EnemyState {
    private Enemy_Rogue enemy;
    public RogueDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Rogue enemy) : base(_enemyBase, _stateMachine, _animBoolName) {
        this.enemy = enemy;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Update() {
        base.Update();
        if (triggerCalled)
            enemy.SelfDestroy();
    }
}
