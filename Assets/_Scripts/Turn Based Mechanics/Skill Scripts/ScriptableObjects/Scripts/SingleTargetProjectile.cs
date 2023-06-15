using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Objects/Single Target Projectile")]
public class SingleTargetProjectile : SkillObject
{
    [Tooltip("The efficacy of the projectile. Damage by default, but accounts for other skill types.")]
    public float rawDamage = 10f;

    [Tooltip("The base travel speed of the projectile.")]
    public float baseSpeed = 1f;

    [Tooltip("The duration of which the projectile will remain active. '0' means the projectile will never despawn (until it exits the map).")]
    public float lifetime = 0f;

    public ProjectileCursor projectileCursor;

    //TODO: Section for Interactions, ActiveEffect, HitEffect, and EndEffect
}
