using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rogue : Enemy {

    [Header("Info. Rogue")]
    public float battleStateMoveSpeed;
    [SerializeField] private GameObject explosivePrefab;
    [SerializeField] private float growthSpeed;
    [SerializeField] private float maxSize;
    #region Estados
    public RogueIdleState IdleState { get; private set; }
    public RogueMoveState MoveState { get; private set; }
    public RogueDeadState DeadState { get; private set; }
    public RogueStunnedState StunnedState { get; private set; }
    public RogueBattleState BattleState { get; private set; }
    #endregion
    protected override void Awake() {
        base.Awake();
        IdleState = new RogueIdleState(this, stateMachine, "Idle", this);
        MoveState = new RogueMoveState(this, stateMachine, "Move", this);
        DeadState = new RogueDeadState(this, stateMachine, "Dead", this);
        StunnedState = new RogueStunnedState(this, stateMachine, "Stunned", this);
        BattleState = new RogueBattleState(this, stateMachine, "MoveFast", this);
    }

    protected override void Start() {
        base.Start();
        stateMachine.Initialize(IdleState);
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
    protected override void Update() {
        base.Update();
    }
    public override void AnimationSpecialAttackTrigger() {
        GameObject newExplosive = Instantiate(explosivePrefab, attackCheck.position, Quaternion.identity);
        newExplosive.GetComponent<Explosive_Controller>().SetupExplosive(stats, growthSpeed, maxSize, attackCheckRadius);
        cd.enabled = false;
        rb.gravityScale = 0;
    }
    public void SelfDestroy() => Destroy(gameObject);
}
