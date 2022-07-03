using Player;
using UnityEngine;

namespace Enemies {
    public class Mob : MonoBehaviour {
        public enum EnemyType {
            Ally,
            Foe
        }

        private Sanity _sanity;
        [SerializeField] private float addToSanity = 50f;
        [SerializeField] private float subtractFromSanity = 50f;

        [SerializeField] private EnemyType type;

        [SerializeField] private float deleteMobDelay = 5f;

        private GameObject _spawner;

        private bool _active;

        private Rigidbody2D _rigidbody;

        private UnityEngine.Light _light;

        private Vector2 _velocity = Vector2.zero;

        private const float KVelocityMultiplier = 50f;

        // private Transform _transform;

        public void SetType(EnemyType typeTo) {
            type = typeTo;

            var color = GetComponent<SpriteRenderer>();

            switch (type) {
                case EnemyType.Foe: {
                    color.color = Color.red;
                    break;
                }

                case EnemyType.Ally: {
                    color.color = Color.cyan;
                    break;
                }
            }
        }

        private void OnDestroy() {
            if (_spawner) {
                _spawner.GetComponent<Spawner>().DeleteMob();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                if (!_sanity) {
                    return;
                }

                switch (type) {
                    case EnemyType.Ally: {
                        _sanity.AddSanity(addToSanity);
                        break;
                    }
                    case EnemyType.Foe: {
                        _sanity.SubtractSanity(subtractFromSanity);
                        break;
                    }
                }

                Destroy(gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (other.CompareTag("LightSource")) {
                _active = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("LightSource")) {
                _active = false;
            }
        }

        private void DeleteIfInactive() {
            if (!_active) {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate() {
            if (!_active) {
                return;
            }

            var currentVelocity = _rigidbody.velocity;

            var lightPos = _light.transform.position;
            var pos = transform.position;

            var velocity = (lightPos - pos) * (Time.fixedDeltaTime * KVelocityMultiplier * Mathf.InverseLerp(
                0.1f,
                1,
                1 / Vector2.Distance(lightPos, pos)
            ));

            _rigidbody.velocity =
                Vector2.SmoothDamp(currentVelocity, velocity, ref _velocity, 0.05f);
        }

        void Start() {
            _spawner = GameObject.FindWithTag("Spawner");
            _light = GameObject.FindWithTag("LightSource").GetComponent<UnityEngine.Light>();
            _rigidbody = GetComponent<Rigidbody2D>();
            var sanity = GameObject.FindWithTag("Sanity");
            _sanity = sanity.GetComponent<Sanity>();
            InvokeRepeating(nameof(DeleteIfInactive), deleteMobDelay, deleteMobDelay);
        }
    }
}
