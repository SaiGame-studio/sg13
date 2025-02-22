using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class LanguageManager : SaiSingleton<LanguageManager>
{
    public List<LanguageData> SupportedLanguages = new List<LanguageData>();
    public LanguageCode FallbackLanguage = LanguageCode.en;
    public LanguageCode CurrentLanguage = LanguageCode.vi;

    private Dictionary<string, string> translationCache = new Dictionary<string, string>();

    public static string T(string text)
    {
        return LanguageManager.Instance.Trans(text);
    }

    protected override void Start()
    {
        base.Start();
        LoadLanguage(LanguageCode.vi);
        LoadLanguage(LanguageCode.en);
    }

    private string LoadJsonFromResources(LanguageCode language)
    {
        string fileName = language.ToString().ToLower();
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile != null)
        {
            return jsonFile.text;
        }
        else
        {
            Debug.LogError($"File {fileName} not found in Resources.");
            return null;
        }
    }

    public void LoadLanguage(LanguageCode languageCode)
    {
        string jsonContent = LoadJsonFromResources(languageCode);
        if (!string.IsNullOrEmpty(jsonContent))
        {
            var loadedLanguage = JsonUtility.FromJson<LanguageData>(jsonContent);
            if (loadedLanguage != null)
            {
                CurrentLanguage = languageCode;
                translationCache.Clear();
                foreach (var entry in loadedLanguage.Translations)
                {
                    if (!translationCache.ContainsKey(entry.Key))
                    {
                        translationCache[entry.Key] = entry.Value;
                    }
                }

                //Debug.Log($"Language {languageCode} loaded successfully.");
            }
            else
            {
                Debug.LogError($"Failed to parse language file for {languageCode}.");
            }
        }
    }

    public string Trans(string key)
    {
        if (translationCache.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            if(this.CurrentLanguage != this.FallbackLanguage) Debug.LogWarning($"Translation for key '{key}' not found.");
            return key;
        }
    }

    [ProButton]
    public void ChangeLanguage(LanguageCode newLanguageCode)
    {
        if (newLanguageCode != this.CurrentLanguage)
        {
            LoadLanguage(newLanguageCode);
        }
    }
}
