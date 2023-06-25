using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : ActorAction
{
    [SerializeField] private SkillObject skill;
    private Vector3 location;

    public SkillAction(SkillObject skill) {
        this.skill = skill;
    }

    public void RunAction(Transform actor, float duration) {
        //TODO: RunAction will likely pull data from a ScriptableObject
    }

    public void StoreLocation(Vector3 location) {
        this.location = location;
    }

    public SkillObject getSkill() {
        return skill;
    }

    public int GetCost() {
        return skill.GetCost();
    }

    public Vector3 GetLocation() {
        return location;
    }
}
