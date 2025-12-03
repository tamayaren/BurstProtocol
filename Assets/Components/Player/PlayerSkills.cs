using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        private Dictionary<SkillLogic, float> cooldownQueue = new Dictionary<SkillLogic, float>();
        
        private void Start()
        {
            this.inputManager.skillActionRequest.AddListener(Perform);
        }

        private void ManageCooldown()
        {
            if (this.cooldownQueue.Count > 0)
            {
                foreach (KeyValuePair<SkillLogic, float> kvp in this.cooldownQueue)
                {
                    kvp.Key.onCooldown = true;
                    this.cooldownManager.TryAdd(kvp.Key, kvp.Value);
                }


                this.cooldownQueue.Clear();
            }
            
            foreach (KeyValuePair<SkillLogic, float> skill in this.cooldownManager)
            {
                SkillLogic skillLogic = skill.Key;
                float timer = skill.Value;
                
                Debug.Log("CheckingTimer " + skill.Value);
                skillLogic.onCooldown = true;
                if (skill.Key == null || timer <= 0)
                {
                    this.cooldownManager.Remove(skillLogic);

                    skillLogic.onCooldown = false;
                    Debug.Log("CanPerformNow");
                    break;
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
            
            this.skills = new SkillLogic[metadata.skillClasses.Length];
            for (int i = 0; i < this.skills.Length; i++)
            {
                string skillName = metadata.skillClasses[i];
                if (skillName == null) continue;
                if (CharacterSkillMetadata.SkillRegistry.TryGetValue(skillName, out Type type))
                {
                    SkillLogic skillLogic = (SkillLogic)Activator.CreateInstance(type);
                    this.skills[i] = skillLogic;
                    skillLogic.Initialize(this.entity);
                    skillLogic.OnCooldown = b =>
                    {
                        
                    };

                    skillLogic.OnStatusChanged = status =>
                    {
                        if (status == SkillStatus.Ended || status == SkillStatus.Cancelled)
                        {
                            Delegate[] invocationList = skillLogic.OnStatusChanged.GetInvocationList();
                            foreach (Delegate invocation in invocationList)
                                Delegate.Remove(skillLogic.OnStatusChanged, invocation);
                        
                            this.CurrentSkillPerforming = null;
                            this.canSkill = true;
                            Debug.Log("Action provoked");
                        }
                    };
                    
                }
            }
            
            Debug.Log("character hooked");
            Debug.Log(this.skills.Length);
            Debug.Log(this.skills[0]?.skillName);
        }
        
        private void Perform(int id)
        {
            Debug.Log(this.skills.Length);
            Debug.Log(id);
            
            Debug.Log("Consider perform:");
            if (!this.canSkill) return;
            Debug.Log("Skill confirmity");
            SkillLogic skill = this.skills[id];
            if (skill == null) return;
            Debug.Log("Skill valid");
            if (skill.onCooldown) return;
            
            bool isValid = skill.Perform(this.entity, this);
            if (isValid && !skill.onCooldown)
            {
                float cooldown = skill.Action(this.entity, this);
                if (!this.cooldownQueue.ContainsKey(skill))
                {
                    this.cooldownQueue.TryAdd(skill, cooldown);
                    SkillDisplay.instance.Cooldown(id, cooldown);
                }
                this.CurrentSkillPerforming = skill;
                
                this.canSkill = false;
            }
        }
    }
}
