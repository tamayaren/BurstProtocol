using UnityEngine;

public class StatCalculator : MonoBehaviour
{
    private static float dashDuration = .125f;
    private static float dashSpeed = 32f;
    public static float CalculateSpeed(float baseSpeed, EntityStats stats) => baseSpeed * (stats.speed * .25f + ((float)stats.endurance/64f));
    public static float CalculateDashCooldown(float baseDashCooldown, EntityStats stats) => 3f;

    public static (float, float) CalculateDash(EntityStats stats)
    {
        float speed = dashSpeed * (1.05f+(stats.speed/255f)+(stats.endurance/64f));
        float duration = dashDuration * (1.25f+(stats.endurance/48f));
        
        return (speed, duration);
    }
}
