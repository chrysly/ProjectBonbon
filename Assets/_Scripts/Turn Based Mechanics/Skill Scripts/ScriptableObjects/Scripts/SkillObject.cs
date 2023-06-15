using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { OFFENSIVE, SUPPORT, OTHER };

public class SkillObject : ScriptableObject {

    [Tooltip("The category the skill falls under. Offensive by default.")]
    [SerializeField] private SkillType skillType = SkillType.OFFENSIVE;

    [Tooltip("Determines how using the projectile is visually displayed on the interface.")]
    [SerializeField] private CursorType cursorType;

    public SkillType getSkillType() { return skillType; }
    public CursorType getCursorType() { return cursorType; }
}
