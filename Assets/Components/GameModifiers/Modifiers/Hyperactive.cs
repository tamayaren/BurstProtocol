
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hyperactive : GameModifierBase
{
    private bool onCooldown = false;
    private EntityStats entityStats;
    private Entity entity;

    private IEnumerator Cooldown()
    {
        this.onCooldown = true;
        yield return new WaitForSeconds(16f - (4f * this.stack));
        this.onCooldown = false;
    }
    
    private IEnumerator SpeedMode()
    {
        Debug.Log("HYPERACTIVE ON");
        
        UINotification.instance.Notify("HYPERACTIVE ON", Color.yellow);
        StartCoroutine(Cooldown());
        int speed = 50 + (20 * this.stack);
        this.entityStats.speed += speed;
        yield return new WaitForSeconds(this.stack);
        Debug.Log("HYPERACTIVE OFF");
        UINotification.instance.Notify("HYPERACTIVE OFF", Color.yellow);
        this.entityStats.speed -= speed;
    }
    
    public override void Initialize()
    {
        this.entityStats = this.gameObject.GetComponent<EntityStats>();
        this.entity = this.gameObject.GetComponent<Entity>();

        float lastHealth = this.entity.Health;
        this.entity.DamageInflicted.AddListener((health) =>
    {
            if (!this.onCooldown) StartCoroutine(SpeedMode());
        });
        Debug.Log(this.name + " assigned");
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
