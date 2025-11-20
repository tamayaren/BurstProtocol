using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class LocalSave : MonoBehaviour
{
    public static SaveFile cachedSave;

    public string SavePref(SaveFile save)
    {
        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString("AccountSave", json);

        return json;
    }

    public SaveFile LoadPref() => JsonUtility.FromJson<SaveFile>(PlayerPrefs.GetString("AccountSave"));
}

public struct SaveFile
{
    public Dictionary<string, EntityStatsSchema> characterUnlocked;
    public Organelle[] organelles;
}