using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBattleState : EnemyState {
    private int moveDir;
    private Transform player;
    private Enemy_Rogue enemy;
    private float defaultSpeed;
    public RogueBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Rogue _enemy) : base(_enemyBase, _stateMachine, _animBoolName) {
        this.enemy = _enemy;
    }

    public override void Enter() {
        base.Enter();
        defaultSpeed = enemy.moveSpeed;
        enemy.moveSpeed = enemy.battleStateMoveSpeed;

        player = PlayerManager.instance.player.transform;
        if (player.GetComponent<PlayerStats>().IsDead)
            enemy.stats.KillEntity();
    }

    public override void Update() {
        base.Update();

        if (enemy.IsPlayerDetected()) {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                stateMachine.ChangeState(enemy.DeadState);

        } else {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                stateMachine.ChangeState(enemy.IdleState);
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit() {
        base.Exit();
        enemy.moveSpeed = defaultSpeed;
    }

    private bool CanAttack() {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown) {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
