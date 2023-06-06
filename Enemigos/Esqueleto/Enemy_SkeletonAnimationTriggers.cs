using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTriggers : MonoBehaviour {
    private Enemy_Skeleton Enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger() {
        Enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Enemy.attackCheck.position, Enemy.attackCheckRadius);

        foreach (var hit in colliders) {
            if (hit.GetComponent<Player>() != null) {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                Enemy.stats.DoDamage(target);
            }
        }
    }

    private void OpenCounterWindow() => Enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => Enemy.CloseCounterAttackWindow();
}
