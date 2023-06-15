using UnityEngine;

/// <summary>
/// Estado del jugador que se activa cuando se realiza un contraataque. Sus características son:
/// Entra en la animación de contraataque.
/// Durante el periodo de tiempo que está en la animación, comprueba si hay enemigos en el área de ataque que
/// estén atacando y que puedan ser aturdidos.
/// </summary>
public class PlayerCounterAttackState : PlayerState {
    private bool canCreateClone;

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        canCreateClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit() {
        base.Exit();


    }

    public override void Update() {
        base.Update();

        player.SetZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders) {
            if (hit.GetComponent<Enemy>() != null) {
                if (hit.GetComponent<Enemy>().CanBeStunned()) {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessfulCounterAttack", true);

                    player.Skill.Parry.UseSkill();

                    if (canCreateClone) {
                        canCreateClone = false;
                        player.Skill.Parry.MakeMirageOnParry(hit.transform);
                    }
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }
}
