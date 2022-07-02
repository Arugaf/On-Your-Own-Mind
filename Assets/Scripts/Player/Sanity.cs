using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Sanity : MonoBehaviour {
        [SerializeField] private float sanity;
        
        [SerializeField] private float maxSanity;
        [SerializeField] private float defaultSanity;

        [SerializeField] private float amountPerTenthOfSecond;
        public void ReduceConsistently() {
            sanity = Mathf.Clamp(sanity - amountPerTenthOfSecond, 0f, maxSanity);
        }

        // Start is called before the first frame update
        void Start() {
            sanity = defaultSanity;
            InvokeRepeating("ReduceConsistently", 0, 0.1f);
        }

        // Update is called once per frame
        void Update() { }
    }
}
