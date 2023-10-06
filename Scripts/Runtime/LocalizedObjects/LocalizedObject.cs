using TMPro;
using UnityEngine;

namespace LESCH
{
    public abstract class LocalizedObject : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _render;

        protected string _text;

        private void OnEnable()
        {
            Localization.LanguageChanged.AddListener(Localize);
        }

        private void Awake()
        {
            _text = _render.text;
        }

        private void Start()
        {
            Localize();
        }

        public void Localize(string key)
        {
            _text = key;
            Localize();
        }

        public void Localize()
        {
            TMP_FontAsset font = Localization.CurrentFont;

            if (font != null) {
                SetFont(font);
            }

            string translation = GetTranslation();

            if (translation == null) {
                return;
            }

            SetText(translation);
        }

        protected virtual void SetText(string translation)
        {
            _render.text = translation;
        }

        protected virtual void SetFont(TMP_FontAsset font)
        {
            _render.font = font;
        }

        protected abstract string GetTranslation();
    }
}

