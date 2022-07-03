using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Player {
    public class Sanity : MonoBehaviour {
        [SerializeField] private float sanity;

        [SerializeField] private float maxSanity;
        [SerializeField] private float defaultSanity;

        [SerializeField] [Range(0f, 1f)] private float fractionOfSecond;
        [SerializeField] private float amountPerFractionOfSecond;

        [SerializeField] private Graphic brain;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;

        private Slider _slider;
        [SerializeField] private RectTransform circularScale;

        private Mutex _sanityMutex = new Mutex();

        public void ReduceConsistently() {
            _sanityMutex.WaitOne();
            sanity = Mathf.Clamp(sanity - amountPerFractionOfSecond, 0f, maxSanity);
            _slider.value = Mathf.InverseLerp(0f, maxSanity, sanity);
            // 630 240
            circularScale.sizeDelta = new Vector2(250, Mathf.Lerp(240, 630, _slider.value));
            _sanityMutex.ReleaseMutex();
        }

        public void AddSanity(float amount) {
            _sanityMutex.WaitOne();
            sanity = Mathf.Clamp(sanity + amount, 0f, maxSanity);
            _slider.value = Mathf.InverseLerp(0f, maxSanity, sanity);
            circularScale.sizeDelta = new Vector2(250, Mathf.Lerp(240, 630, _slider.value));
            _sanityMutex.ReleaseMutex();
        }

        public void SubtractSanity(float amount) {
            _sanityMutex.WaitOne();
            sanity = Mathf.Clamp(sanity - amount, 0f, maxSanity);
            _slider.value = Mathf.InverseLerp(0f, maxSanity, sanity);
            _sanityMutex.ReleaseMutex();
        }

        void Start() {
            sanity = defaultSanity;
            InvokeRepeating(nameof(ReduceConsistently), 0, fractionOfSecond);
            _slider = GetComponent<Slider>();
        }

        void Update() {
            var r = Mathf.Lerp(startColor.r, endColor.r, _slider.value);
            var g = Mathf.Lerp(startColor.g, endColor.g, _slider.value);
            var b = Mathf.Lerp(startColor.b, endColor.b, _slider.value);
            brain.color = new Color(r, g, b);
        }
    }
}
