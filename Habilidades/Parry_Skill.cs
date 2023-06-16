using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill {

    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked { get; private set; }


    [SerializeField] private UI_SkillTreeSlot restoreUnlockButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPerentage;
    public bool restoreUnlocked { get; private set; }


    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    public bool ParryWithMirageUnlocked { get; private set; }

    protected override void CheckUnlock() {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();
    }

    public override void UseSkill() {
        base.UseSkill();


        if (restoreUnlocked) {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthPerentage);
            player.stats.IncreaseHealthBy(restoreAmount);
        }

    }

    protected override void Start() {
        base.Start();
        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }

    private void UnlockParry() {
        if (parryUnlockButton.unlocked)
            parryUnlocked = true;
    }

    private void UnlockParryRestore() {
        if (restoreUnlockButton.unlocked)
            restoreUnlocked = true;
    }

    private void UnlockParryWithMirage() {
        if (parryWithMirageUnlockButton.unlocked)
            ParryWithMirageUnlocked = true;
    }

    public void MakeMirageOnParry(Transform _respawnTransform) {
        if (ParryWithMirageUnlocked)
            SkillManager.instance.Clone.CreateCloneWithDelay(_respawnTransform);
    }

}
