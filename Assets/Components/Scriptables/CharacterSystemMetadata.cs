using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Metadata/Character")]
public class CharacterSystemMetadata : ScriptableObject
{
    public int id;
    public AnimatorController animator;
    public Sprite sprite;

    public int baseHealth;

    public int baseAttack;
    public int baseDefense;
    public int basePathAttack;
    public int basePathDefense;
 
    public int baseSpeed;
    public int baseEndurance;

    public float baseCritDamage;
    public float baseCritRate;

    public float dashRange;
    public float dashCooldown;

    public float fireRate;
    public float projectileSpeed;
    public float baseAttackDamage;
    public FireMode baseFireMode;

    public AnimationClip[] animations;
}

public enum FireMode
{
    Normal,
    Automatic,
    SemiAutomatic,
    Dynamic
}
