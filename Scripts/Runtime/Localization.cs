using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LESCH
{
    public class Localization : MonoBehaviour
    {
        public static UnityEvent LanguageChanged = new();
        public static string CurrentLanguage;

        [SerializeField] private string _defaultLanguageKey = "en";
        [SerializeField] private string _dictionaryPath = "/LocalizationDictionary.xml";
        [SerializeField] private LocalizationStorage _storage;
        [SerializeField] private List<LocalizationFontItem> _fonts;

        private static string _defaultLanguageKeyInternal;
        private static ILocalizationStorage _storageInternal;
        private static List<LocalizationFontItem> _fontsInternal;
        private static TMP_FontAsset _currentFont;

        public static TMP_FontAsset CurrentFont => _currentFont;

        private void Awake()
        {
            _fontsInternal = new List<LocalizationFontItem>(_fonts);
            _storageInternal = _storage;

            _storageInternal.LoadDictionary(_dictionaryPath);
        }

        private void Start()
        {
            SetLanguage(_defaultLanguageKey);
        }

        public static void SetLanguage(string languageKey)
        {
            if (languageKey == CurrentLanguage) {
                return;
            }

            CurrentLanguage = languageKey ?? _defaultLanguageKeyInternal;

            SetCurrentFont();

            LanguageChanged.Invoke();
        }

        public static string GetTranslationInCurrentLanguage(string translationKey)
        {
            return GetTranslation(CurrentLanguage, translationKey);
        }

        public static string GetLanguageName(string languageKey)
        {
            return _storageInternal.GetLanguageName(languageKey);
        }

        public static List<string> GetAllLanguages()
        {
            return _storageInternal.GetAllLanguages();
        }

        public static List<string> GetAllTranslationKeys()
        {
            return _storageInternal.GetAllTranslationKeys();
        }

        public static TMP_FontAsset GetFontForLanguage(string languageKey)
        {
            return _fontsInternal.Find(font => font.Language == languageKey)?.Font;
        }

        private static string GetTranslation(string languageKey, string translationKey)
        {
            string translation = _storageInternal.GetTranslation(languageKey, translationKey);

            return translation == null ? translationKey : translation;
        }

        private static void SetCurrentFont()
        {
            _currentFont = _fontsInternal.Find(font => font.Language == CurrentLanguage)?.Font;
        }
    }
}
