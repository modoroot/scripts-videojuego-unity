using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy {
    #region Estados

    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }
    public SkeletonBattleState BattleState { get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    public SkeletonStunnedState StunnedState { get; private set; }
    public SkeletonDeadState DeadState { get; private set; }
    #endregion

    protected override void Awake() {
        base.Awake();

        IdleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        MoveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        BattleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        AttackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        StunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        DeadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start() {
        base.Start();
        stateMachine.Initialize(IdleState);
    }

    protected override void Update() {
        base.Update();

    }

    public override bool CanBeStunned() {
        if (base.CanBeStunned()) {
            stateMachine.ChangeState(StunnedState);
            return true;
        }
        return false;
    }

    public override void Die() {
        base.Die();
        stateMachine.ChangeState(DeadState);

    }
}
