using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSet
{
    public class PlayerSkills : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager inputManager;
        [SerializeField] private Entity entity;
        
        public bool canSkill = true;
        [SerializeField] public SkillLogic[] skills;

        public SkillLogic CurrentSkillPerforming;
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
                
                if (skill.Key == null || timer <= 0)
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

            if (this.CurrentSkillPerforming != null) 
                this.CurrentSkillPerforming.Update(this.entity, this);
        }
        
        public void HookCharacterSkills(PlayerCharacterIdentifier character)
        {
            CharacterSystemMetadata metadata = character.currentCharacter;
            
            this.skills = new SkillLogic[metadata.skills.Length];
            for (int i = 0; i < this.skills.Length; i++)
            {
                SkillLogic skillLogic = metadata.skills[i];

                Debug.Log(skillLogic.skillName);
                this.skills[i] = skillLogic;
            }
            
            Debug.Log("character hooked");
            Debug.Log(this.skills.Length);
            Debug.Log(this.skills[0].skillName);
        }
        
        private void Perform(int id)
        {
            Debug.Log(this.skills.Length);
            Debug.Log(id);
            
            Debug.Log("Consider perform:");
            if (!this.canSkill) return;
            
            SkillLogic skill = this.skills[id];
            if (skill == null) return;
            
            bool isValid = skill.Perform(this.entity, this);
            if (isValid)
            {
                this.cooldownManager.Add(skill, skill.cooldownDuration);
                skill.OnStatusChanged = (status) =>
                {
                    if (status == SkillStatus.Ended || status == SkillStatus.Cancelled)
                    {
                        Delegate[] invocationList = skill.OnStatusChanged.GetInvocationList();
                        foreach (Delegate invocation in invocationList)
                            Delegate.Remove(skill.OnStatusChanged, invocation);
                        
                        this.CurrentSkillPerforming = null;
                        this.canSkill = true;
                    }
                };
                this.canSkill = false;
            }
        }
    }
}
