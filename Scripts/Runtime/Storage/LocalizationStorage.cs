using System;
using System.Collections.Generic;
using UnityEngine;

namespace LESCH
{
    public enum FileSerializer
    {
        XmlFileSerializer,
        JsonFileSerializer
    }

    public class LocalizationStorage : MonoBehaviour, ILocalizationStorage
    {
        protected const string DICTIONARY_NOT_INITIALIZED_EXCEPTION = "Dictionary is not initialized";
        protected const string LANGUAGE_DOES_NOT_EXIST = "Passed language key does not exist.";
        protected const string LANGUAGE_NAME_TRANSLATION_KEY = "english";

        [SerializeField] protected FileSerializer _selectedFileSerializer;
        [SerializeField] protected bool _isCustomSerializerUsed;

        protected string _dictionaryPath;
        protected IFileSerializer _fileSerializer;
        protected LocalizationDictionary _dictionary;

        public virtual void LoadDictionary(string dictionaryPath)
        {
            string namespaceName = _isCustomSerializerUsed ? "" : "LESCH.";
            Type fileSerializerType = Type.GetType(namespaceName + _selectedFileSerializer.ToString());
            _fileSerializer = (IFileSerializer)Activator.CreateInstance(fileSerializerType);

            _dictionaryPath = Application.dataPath + dictionaryPath;
            _dictionary = _fileSerializer.Load<LocalizationDictionary>(_dictionaryPath);

            if (_dictionary == null) {
                _dictionary = new LocalizationDictionary();
            }
        }

        public string GetLanguageName(string languageKey)
        {
            if (!LanguageExists(languageKey)) {
                return null;
            }

            return GetLocalizationTranslation(languageKey, LANGUAGE_NAME_TRANSLATION_KEY)?.Value;
        }

        public string GetTranslation(string languageKey, string translationKey)
        {
            if (!LanguageExists(languageKey)) {
                return null;
            }

            return GetLocalizationTranslation(languageKey, translationKey)?.Value;
        }

        public string GetFirstLanguageKey()
        {
            if (_dictionary == null) {
                throw new NullReferenceException(DICTIONARY_NOT_INITIALIZED_EXCEPTION);
            }

            if (_dictionary.Languages.Count <= 0) {
                return null;
            }

            return _dictionary.Languages[0].Key;
        }

        public List<string> GetAllLanguages()
        {
            if (_dictionary == null) {
                throw new NullReferenceException(DICTIONARY_NOT_INITIALIZED_EXCEPTION);
            }

            List<string> languages = new(_dictionary.Languages.Count);

            _dictionary.Languages.ForEach(language => languages.Add(language.Key));

            return languages;
        }

        public List<string> GetAllTranslationKeys()
        {
            LocalizationDictionary.LocalizationLanguage language = GetLocalizationLanguage(GetFirstLanguageKey());

            if (language == null || language.Translations.Count == 0) {
                return new List<string>();
            }

            List<string> translations = new(language.Translations.Count);

            language.Translations.ForEach(translation => translations.Add(translation.Key));

            return translations;
        }

        protected bool LanguageExists(string languageKey)
        {
            return GetLocalizationLanguage(languageKey) != null;
        }

        protected bool KeyExists(string languageKey, string translationKey)
        {
            if (!LanguageExists(languageKey)) {
                return false;
            }

            LocalizationDictionary.LocalizationLanguage language = GetLocalizationLanguage(GetFirstLanguageKey());

            return language.Translations.Find(translation => translation.Key == translationKey) != null;
        }

        protected LocalizationDictionary.LocalizationLanguage GetLocalizationLanguage(string languageKey)
        {
            if (_dictionary == null) {
                throw new NullReferenceException(DICTIONARY_NOT_INITIALIZED_EXCEPTION);
            }

            return _dictionary.Languages.Find(language => language.Key == languageKey);
        }

        protected LocalizationDictionary.LocalizationLanguage.LocalizationTranslation GetLocalizationTranslation(string languageKey, string translationKey)
        {
            return GetLocalizationLanguage(languageKey).Translations.Find(translation => translation.Key == translationKey);
        }
    }
}
