using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Clase que contiene todas las propiedades del jugador y controla su comportamiento
/// a partir de los estados que se le asignan.
/// </summary>
public class Player : Entity {
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    public float swordReturnImpact;
    private float defaultMoveSpeed;
    private float defaultJumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    private float defaultDashSpeed;
    public float DashDir { get; private set; }


    public SkillManager Skill { get; private set; }
    public GameObject Sword { get; private set; }


    #region Estados
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerWallSlideState WallSlide { get; private set; }
    public PlayerWallJumpState WallJump { get; private set; }
    public PlayerDashState DashState { get; private set; }

    public PlayerPrimaryAttackState PrimaryAttack { get; private set; }
    public PlayerCounterAttackState CounterAttack { get; private set; }

    public PlayerAimSwordState AimSword { get; private set; }
    public PlayerCatchSwordState CatchSword { get; private set; }
    public PlayerBlackholeState BlackHole { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    #endregion

    protected override void Awake() {
        base.Awake();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlide = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJump = new PlayerWallJumpState(this, StateMachine, "Jump");

        PrimaryAttack = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        CounterAttack = new PlayerCounterAttackState(this, StateMachine, "CounterAttack");

        AimSword = new PlayerAimSwordState(this, StateMachine, "AimSword");
        CatchSword = new PlayerCatchSwordState(this, StateMachine, "CatchSword");
        BlackHole = new PlayerBlackholeState(this, StateMachine, "Jump");

        DeadState = new PlayerDeadState(this, StateMachine, "Die");
    }

    protected override void Start() {
        base.Start();

        Skill = SkillManager.instance;

        StateMachine.Initialize(IdleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }


    protected override void Update() {
        if (Time.timeScale == 0)
            return;
        base.Update();
        StateMachine.CurrentState.Update();
        CheckForDashInput();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.instance.UseFlask();
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration) {
        moveSpeed *= (1 - _slowPercentage);
        jumpForce *= (1 - _slowPercentage);
        dashSpeed *= (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke(nameof(ReturnDefaultSpeed), _slowDuration);

    }

    protected override void ReturnDefaultSpeed() {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }

    public void AssignNewSword(GameObject _newSword) {
        Sword = _newSword;
    }

    public void CatchTheSword() {
        StateMachine.ChangeState(CatchSword);
        Destroy(Sword);
    }

    public IEnumerator BusyFor(float _seconds) {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForDashInput() {
        if (IsWallDetected())
            return;

        if (Skill.Dash.DashUnlocked == false)
            return;


        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.Dash.CanUseSkill()) {

            DashDir = Input.GetAxisRaw("Horizontal");

            if (DashDir == 0)
                DashDir = FacingDir;


            StateMachine.ChangeState(DashState);
        }
    }

    public override void Die() {
        base.Die();

        StateMachine.ChangeState(DeadState);
    }
}
