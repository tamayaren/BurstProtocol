using System;
using System.Collections.Generic;
using Game.Mechanics;
using Game.Mechanics.Organelles;
using UnityEngine;
using SimpleJSON;

namespace Game.Account
{
    public class LocalSave : MonoBehaviour
    {
        public static LocalSave instance;
        public static SaveFile cachedSave;

        public void Awake() => instance = this;
        public string SavePref(SaveFile save)
        {
            string json = JsonUtility.ToJson(save);
            PlayerPrefs.SetString("AccountSave", json);
            
            cachedSave = save;
            return json;
        }

        public SaveFile LoadPref()
        {
           SaveFile saveFile = JsonUtility.FromJson<SaveFile>(PlayerPrefs.GetString("AccountSave"));
           
           cachedSave = saveFile;
           return saveFile;
        }
    }

    public struct SaveFile
    {
        public string username;
        
        public Dictionary<string, EntityStatsSchema> characterUnlocked;
        public Organelle[] organelles;
        public Guid[] equippedOrganelle;
    }
}
