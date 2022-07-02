using UnityEngine;

namespace Player {
    public class MovementController : MonoBehaviour {
        private Rigidbody2D _playerRigidbody;
        private SpriteRenderer _spriteRenderer;

        private float _verticalMove;
        private float _horizontalMove;

        [Range(0.1f, 1f)] [SerializeField] private float verticalVelocityMultiplier = 0.1f;
        [Range(0.1f, 1f)] [SerializeField] private float horizontalVelocityMultiplier = 0.1f;
        private const float KVelocityMultiplier = 1000f;


        private Vector2 _playerVelocity;
        private Vector2 _velocity = Vector2.zero;
        [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

        public void Start() {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update() {
            _verticalMove = Input.GetAxisRaw("Vertical");
            _horizontalMove = Input.GetAxisRaw("Horizontal");

            {
                if (_horizontalMove > 0) {
                    _spriteRenderer.flipX = false;
                }

                if (_horizontalMove < 0) {
                    _spriteRenderer.flipX = true;
                }
            }
        }

        public void FixedUpdate() {
            var currentPlayerVelocity = _playerRigidbody.velocity;

            _playerVelocity = new Vector2(
                _horizontalMove * Time.fixedDeltaTime * horizontalVelocityMultiplier * KVelocityMultiplier,
                _verticalMove * Time.fixedDeltaTime * verticalVelocityMultiplier * KVelocityMultiplier
                );

            _playerRigidbody.velocity =
                Vector2.SmoothDamp(currentPlayerVelocity, _playerVelocity, ref _velocity, movementSmoothing);
        }
    }
}
