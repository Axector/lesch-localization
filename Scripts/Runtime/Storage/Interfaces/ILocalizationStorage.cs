using System.Collections.Generic;

namespace LESCH
{
    public interface ILocalizationStorage
    {
        public void LoadDictionary(string dictionaryPath);
        public string GetLanguageName(string languageKey);
        public string GetTranslation(string languageKey, string translationKey);
        public string GetFirstLanguageKey();
        public List<string> GetAllLanguages();
        public List<string> GetAllTranslationKeys();
    }
}
