using UnityEngine;

public class StatCalculator : MonoBehaviour
{
    public static float CalculateSpeed(float baseSpeed, EntityStats stats) => baseSpeed * (stats.speed * .25f + ((float)stats.endurance/64f));
    public static float CalculateDashCooldown(float baseDashCooldown, EntityStats stats) => 1f;
}
