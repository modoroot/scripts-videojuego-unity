using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {
    public static SkillManager instance;


    public Dash_Skill Dash { get; private set; }
    public Clone_Skill Clone { get; private set; }
    public Sword_Skill Sword { get; private set; }
    public Blackhole_Skill Blackhole { get; private set; }
    public Parry_Skill Parry { get; private set; }

    private void Awake() {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start() {
        Dash = GetComponent<Dash_Skill>();
        Clone = GetComponent<Clone_Skill>();
        Sword = GetComponent<Sword_Skill>();
        Blackhole = GetComponent<Blackhole_Skill>();
        Parry = GetComponent<Parry_Skill>();
    }
}
