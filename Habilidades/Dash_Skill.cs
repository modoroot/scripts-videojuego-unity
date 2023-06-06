using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill {

    [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
    public bool DashUnlocked { get; private set; }


    [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;
    public bool CloneOnDashUnlocked { get; private set; }


    [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;
    public bool CloneOnArrivalUnlocked { get; private set; }


    protected override void CheckUnlock() {
        UnlockDash();
        UnlockCloneOnDash();
        UnlockCloneOnArrival();
    }
    public override void UseSkill() {
        base.UseSkill();
    }

    protected override void Start() {
        base.Start();

        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
    }


    private void UnlockDash() {
        if (dashUnlockButton.unlocked)
            DashUnlocked = true;
    }

    private void UnlockCloneOnDash() {
        if (cloneOnDashUnlockButton.unlocked)
            CloneOnDashUnlocked = true;
    }

    private void UnlockCloneOnArrival() {
        if (cloneOnArrivalUnlockButton.unlocked)
            CloneOnArrivalUnlocked = true;
    }


    public void CloneOnDash() {
        if (CloneOnDashUnlocked)
            SkillManager.instance.Clone.CreateClone(player.transform, Vector3.zero);
    }

    public void CloneOnArrival() {
        if (CloneOnArrivalUnlocked)
            SkillManager.instance.Clone.CreateClone(player.transform, Vector3.zero);
    }
}
