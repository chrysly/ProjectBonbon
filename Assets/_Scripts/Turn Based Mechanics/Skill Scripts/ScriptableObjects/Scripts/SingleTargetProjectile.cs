 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Objects/Single Target Projectile")]
public class SingleTargetProjectile : SkillObject
{
    [Tooltip("The efficacy of the projectile. Damage by default, but accounts for other skill types.")]
    [SerializeField] private float rawDamage = 10f;

    [Tooltip("The base travel speed of the projectile.")]
    [SerializeField] private float baseSpeed = 1f;

    [Tooltip("The duration of which the projectile will remain active. '0' means the projectile will never despawn (until it exits the map).")]
    [SerializeField] private float lifetime = 0f;

    [Tooltip("A cursor prefab that is visually displayed when aiming the skill. Can be empty if only the line should be rendered.")]
    [SerializeField] private GameObject cursorPrefab;

    //CURSOR ACTIONS
    protected CursorType cursor = new ProjectileCursor();

    public override void InitSkillDisplay(CursorManager cursorManager) {
        cursorManager.CreateCursor(cursorPrefab);
        cursorManager.EnableLine(true);
    }

    //TODO: Section for Interactions, ActiveEffect, HitEffect, and EndEffect
}
