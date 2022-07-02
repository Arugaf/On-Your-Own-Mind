using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
    public class Mob : MonoBehaviour {
        public enum EnemyType {
            Ally,
            Foe
        }

        [SerializeField] private EnemyType type;

        // private Transform _transform;

        public Mob(EnemyType type, Vector2 position) {
            this.type = type;
        }

        // Start is called before the first frame update
        void Start() {
            // _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update() { }
    }
}
