using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { OFFENSIVE, SUPPORT, OTHER };

public abstract class SkillObject : ScriptableObject {

    [Tooltip("The string ID of the skill. See drive for naming conventions.")]
    [SerializeField] private int skillID = 0;

    [Tooltip("The name of the skill. Type how this skill name would appear in game.")]
    [SerializeField] private string skillName = "VANILLA";

    [Tooltip("The category the skill falls under. Offensive by default.")]
    [SerializeField] private SkillType skillType = SkillType.OFFENSIVE;

    [Tooltip("The action point cost of the skill. Should not exceed max action turns.")]
    [SerializeField] private int cost = 1;

    private CursorType cursor;

    public abstract void InitSkillDisplay(CursorManager cursorManager);
    public void RunSkillDisplay(CursorManager cursorManager, Transform actor) {
        cursorManager.DrawCursor(actor);
    }
    public void DisableSkillDisplay(CursorManager cursorManager) {
        cursorManager.DeleteCursor();
    }

    public virtual SkillAction GenerateSkillAction() {
        SkillAction skillAction = new(this);
        return skillAction;
    }

    public int GetSkillID() { return skillID; }
    public string GetSkillName() { return skillName; }
    public SkillType GetSkillType() { return skillType; }
    public CursorType GetCursorType() { return cursor; }

    public int GetCost() { return cost; }
}
