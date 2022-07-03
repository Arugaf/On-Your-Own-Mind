using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Light {
    public class LightController : MonoBehaviour {
        [SerializeField] private UnityEngine.Light lightComponent;

        public float intensity;
        public float intensityAddableAmount;
        [HideInInspector] public bool active = false;

        void Awake() {
            // GetComponent<UnityEngine.Light>();

            lightComponent.intensity = 0;
        }

        void Start() {
            active = true;
        }

        public void Update() {
            switch (active) {
                case true when lightComponent.intensity < intensity:
                case false:
                    lightComponent.intensity += intensityAddableAmount;
                    break;
            }

            if (!active && lightComponent.intensity <= 0.1f) {
                SceneController.LoadScene(SceneController.Scene.GameOverScene);
            }
        }
    }
}
