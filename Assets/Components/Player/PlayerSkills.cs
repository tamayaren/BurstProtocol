using System.Collections.Generic;
using UnityEngine;

namespace SkillSet
{
    public class PlayerSkills : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager inputManager;
        [SerializeField] private Entity entity;
        
        public bool canSkill = true;
        public SkillLogic[] skills = new SkillLogic[3];

        private Dictionary<SkillLogic, float> cooldownManager = new Dictionary<SkillLogic, float>();
        
        private void Start()
        {
            this.inputManager.skillActionRequest.AddListener(Perform);
        }

        private void ManageCooldown()
        {
            foreach (KeyValuePair<SkillLogic, float> skill in this.cooldownManager)
            {
                
                SkillLogic skillLogic = skill.Key;
                float timer = skill.Value;
                
                if (!skill.Key || timer <= 0)
                {
                    this.cooldownManager.Remove(skillLogic);

                    skillLogic.onCooldown = false;
                    continue;
                }
                
                this.cooldownManager[skillLogic] -= Time.deltaTime;
            }
        }

        private void Update()
        {
            if (this.cooldownManager.Count > 0)
                ManageCooldown();
        }
        
        public void HookCharacterSkills(PlayerCharacterIdentifier character)
        {
            CharacterSystemMetadata metadata = character.currentCharacter;
            
            this.skills = new SkillLogic[metadata.skills.Length];
            for (int i = 0; i < this.skills.Length; i++)
            {
                SkillLogic skillLogic = metadata.skills[i];
                SkillLogic skillInstance = this.gameObject.AddComponent(skillLogic.GetType()) as SkillLogic;
                
                this.skills[i] = skillInstance;
            }
        }
        
        private void Perform(int id)
        {
            Debug.Log(this.skills.Length);
            Debug.Log(id);
            if (!this.canSkill) return;
            
            SkillLogic skill = this.skills[id];
            if (!skill) return;
            
            bool isValid = skill.Perform(this.entity, this);
            if (isValid)
            {
                this.cooldownManager.Add(skill, skill.cooldownDuration);
                skill.OnStatusChanged.AddListener((status) =>
                {
                    if (status == SkillStatus.Ended || status == SkillStatus.Cancelled)
                    {
                        skill.OnStatusChanged.RemoveAllListeners();
                        this.canSkill = true;
                    }
                });
                this.canSkill = false;
            }
        }
    }
}
