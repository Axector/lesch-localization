using System.Collections.Generic;
using System.Xml.Serialization;

namespace LESCH
{
    [System.Serializable]
    [XmlRoot("LocalizationDictionary")]
    public class LocalizationDictionary
    {
        [System.Serializable]
        public class LocalizationLanguage
        {
            [System.Serializable]
            public class LocalizationTranslation
            {
                [XmlAttribute]
                public string Key;
                [XmlAttribute]
                public string Value;
            }

            [XmlAttribute]
            public string Key;
            public List<LocalizationTranslation> Translations = new();
        }

        public List<LocalizationLanguage> Languages = new();
    }
}
