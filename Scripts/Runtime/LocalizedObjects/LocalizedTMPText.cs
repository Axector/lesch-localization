using TMPro;
using UnityEngine;

namespace LESCH
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedTMPText : LocalizedObject
    {
        private void Awake()
        {
            if (_render == null) {
                _render = GetComponent<TMP_Text>();
            }

            _text = _render.text;
        }

        protected override string GetTranslation()
        {
            return Localization.GetTranslationInCurrentLanguage(_text);
        }
    }
}
