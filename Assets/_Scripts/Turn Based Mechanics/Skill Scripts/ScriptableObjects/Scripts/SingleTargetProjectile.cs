 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Objects/Single Target Projectile")]
public class SingleTargetProjectile : SkillObject
{

    [Header("Projectile Traits")]
    [Tooltip("The efficacy of the projectile. Damage by default, but accounts for other skill types.")]
    [SerializeField] private float rawDamage = 10f;

    [Tooltip("The base travel speed of the projectile.")]
    [SerializeField] private float baseSpeed = 1f;

    [Tooltip("The duration of which the projectile will remain active. '0' means the projectile will never despawn (until it exits the map).")]
    [SerializeField] private float lifetime = 0f;

    [Header("Projectile Display")]
    [Tooltip("A cursor prefab that is visually displayed when aiming the skill. Can be an empty GameObject if the projectile should not display a reticle.")]
    [SerializeField] private GameObject cursorPrefab;

    [Tooltip("A prefab of a LineRenderer that displays the direction and distance of a projectile.")]
    [SerializeField] private GameObject linePrefab;

    //CURSOR ACTIONS
    protected CursorType cursor = new ProjectileCursor();

    public override void InitSkillDisplay(ActionDisplay display) {
        display.CreateDisplay(cursorPrefab);
        display.EnableLine(linePrefab);
    }

    //TODO: Section for Interactions, ActiveEffect, HitEffect, and EndEffect
}
