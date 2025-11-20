using System;
using System.Collections.Generic;
using Game.Account;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

namespace Game.Mechanics.Organelles
{
    public class OrganelleManager : MonoBehaviour
    {
        public Organelle[] organelles;
        public Dictionary<OrganelleType, Organelle> equipped;
        
        public void Start()
        {
            SaveFile save = LocalSave.instance.LoadPref();

            this.organelles = save.organelles;
            GetOnEquip();
        }

        public void GetOnEquip()
        {
            Dictionary<OrganelleType, Organelle> organelleEquip = new Dictionary<OrganelleType, Organelle>();
            List<Guid> guidEquipped = new List<Guid>(LocalSave.cachedSave.equippedOrganelle);
            
            foreach (Organelle organelle in this.organelles)
            {
                bool isEquipped = guidEquipped.Contains(organelle.organelleIdentifier);
                if (isEquipped)
                    organelleEquip.TryAdd(organelle.type, organelle);
            }
            
            this.equipped = organelleEquip;
        }

        public Organelle ServeOrganelleRandom()
        {
            Random random = new Random();
            Array organelleTypes = Enum.GetValues(typeof(OrganelleType));
            
            Organelle organelle = new Organelle
            {
                organelleIdentifier = Guid.NewGuid(),
                type = (OrganelleType)organelleTypes.GetValue(random.Next(organelleTypes.Length))
            };

            return organelle;
        }
    }
}
