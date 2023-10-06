using System;
using TMPro;
using UnityEngine;

namespace LESCH
{
    [Serializable]
    public class LocalizationFontItem
    {
        [SerializeField] private string _language;
        [SerializeField] private TMP_FontAsset _font;

        public string Language => _language;
        public TMP_FontAsset Font => _font;
    }
}
