using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
    [SerializeField] protected LayerMask whatIsPlayer;


    [Header("Info. aturdimiento")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Info movimiento")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultMoveSpeed;

    [Header("Info ataques")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }
    private Player player;
    public string lastAnimBoolName { get; private set; }
    protected override void Awake() {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
    }

    protected override void Update() {
        base.Update();


        stateMachine.CurrentState.Update();

    }

    public virtual void AssignLastAnimName(string _animBoolName) => lastAnimBoolName = _animBoolName;


    public override void SlowEntityBy(float _slowPercentage, float _slowDuration) {
        moveSpeed *= (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke(nameof(ReturnDefaultSpeed), _slowDuration);
    }

    protected override void ReturnDefaultSpeed() {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
    }

    public virtual void FreezeTime(bool _timeFrozen) {
        if (_timeFrozen) {
            moveSpeed = 0;
            anim.speed = 0;
        } else {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }

    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));

    protected virtual IEnumerator FreezeTimerCoroutine(float _seconds) {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }

    #region Contrataque
    public virtual void OpenCounterAttackWindow() {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow() {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    #endregion

    public virtual bool CanBeStunned() {
        if (canBeStunned) {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
    public virtual void AnimationSpecialAttackTrigger() { }
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDir, 5, whatIsPlayer);
    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * FacingDir, transform.position.y));
    }
}
