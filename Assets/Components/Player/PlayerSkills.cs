using System;
using System.Collections;
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
        
        private void Start()
        {
            this.inputManager.skillActionRequest.AddListener(Perform);
        }

        private void Update()
        {
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

        public List<string> cooldowns = new List<string>();
        public IEnumerator Cooldown(string name, float cooldownDuration)
        {
            this.cooldowns.Add(name);
            Debug.Log("MOVE PERFORMANCE " + name);
            yield return new WaitForSeconds(cooldownDuration);
            Debug.Log("MOVE PERFORMANCE 2 " + name);
            this.cooldowns.Remove(name);
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
            if (!this.cooldowns.Contains(id.ToString()))
            {
                float cooldown = skill.Action(this.entity, this);

                Aki_Skill1_Mono skillMono = GetComponent<Aki_Skill1_Mono>();
                skillMono.Initialize(this.transform.Find("Weapon"));
                skillMono.PerformMove();
                this.CurrentSkillPerforming = skill;
                SkillDisplay.instance.Cooldown(id, cooldown);

                StartCoroutine(Cooldown(id.ToString(), cooldown));
            }
        }
    }
}
