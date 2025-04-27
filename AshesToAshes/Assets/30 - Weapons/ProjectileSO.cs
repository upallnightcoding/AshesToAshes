using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "A2A/Projectiles")]
public class ProjectileSO : MonoBehaviour
{
    public string projectileName;

    public GameObject projectilePrefab;

    public GameObject impactPrefab;

    public float duration;
}
