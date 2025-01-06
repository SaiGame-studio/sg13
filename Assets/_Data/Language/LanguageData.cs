using System.Collections.Generic;

[System.Serializable]
public class LanguageData
{
    public string LanguageCode;
    public List<TranslationEntry> Translations = new List<TranslationEntry>();
}