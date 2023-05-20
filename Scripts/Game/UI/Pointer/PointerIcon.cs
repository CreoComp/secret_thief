using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Pointer
{
    public class PointerIcon : MonoBehaviour
    {
        public float speedAppearIcon = 4f;
        [SerializeField] Image _image;
        bool _isShown;

        private void Awake()
        {
            _image.enabled = false;
            _isShown = false;
        }

        public void SetIconPosition(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Show()
        {
            if (_isShown) return;
            _isShown = true;
            StopAllCoroutines();
            StartCoroutine(ShowProcess());
        }

        public void Hide()
        {
            if (!_isShown) return;
            _isShown = false;

            StopAllCoroutines();
            StartCoroutine(HideProcess());
        }

        IEnumerator ShowProcess()
        {
            _image.enabled = true;
            transform.localScale = Vector3.zero;
            for (float t = 0; t < 1f; t += Time.deltaTime * speedAppearIcon)
            {
                transform.localScale = Vector3.one * t;
                yield return null;
            }
            transform.localScale = Vector3.one;
        }

        IEnumerator HideProcess()
        {

            for (float t = 0; t < 1f; t += Time.deltaTime * speedAppearIcon)
            {
                transform.localScale = Vector3.one * (1f - t);
                yield return null;
            }
            _image.enabled = false;
        }

    }
}