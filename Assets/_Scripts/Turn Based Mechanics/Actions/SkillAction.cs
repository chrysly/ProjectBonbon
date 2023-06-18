using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : ActorAction
{
    [SerializeField] private SkillObject skill;

    public SkillAction(SkillObject skill) {
        this.skill = skill;
    }

    public void RunAction(Transform actor, float duration) {
        //TODO: RunAction will likely pull data from a ScriptableObject
    }

    public int GetCost() {
        return skill.GetCost();
    }
}
