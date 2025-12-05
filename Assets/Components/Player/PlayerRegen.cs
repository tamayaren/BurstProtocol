using UnityEngine;

public class PlayerRegen : MonoBehaviour
{
    private Entity entity;
    private float regenElapse = 0.5f;
    private int regenAdd = 12;
    
    private float t;
    private void Start()
    {
        this.entity = GetComponent<Entity>();
    }

    private void Update()
    {
        if (this.entity.Health >= this.entity.MaxHealth) return;
        this.t += Time.deltaTime;

        if (this.t >= this.regenElapse)
        {
            int regenAdd = this.regenAdd;
            if (PlayerBuffs.instance.buffs.ContainsKey("Radicals"))
            {
                Radicals buff = PlayerBuffs.instance.buffsLogics["Radicals"] as Radicals;

                regenAdd = (int)(regenAdd * (0.2f * buff.stack));
            }
            
            this.entity.Health += regenAdd;
            this.t = 0f;
        }
    }
}
