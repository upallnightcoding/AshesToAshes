using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "A2A/Projectiles")]
public class ProjectileSO : ScriptableObject
{
    public string projectileName;

    public GameObject projectileTrailPrefab = null;

    public GameObject impactPrefab = null;

    public float duration = 1.5f;

    public float speed = 200.0f;
}
