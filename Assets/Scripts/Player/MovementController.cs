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

        private Sprite _front;
        private Sprite _back;
        private Sprite _left;
        private Sprite _right;

        private AudioSource _movementSound;

        // private SpriteRenderer _renderer;

        public void Start() {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _front = Resources.Load<Sprite>("Player/PlayerFront");
            _back = Resources.Load<Sprite>("Player/PlayerBack");
            _left = Resources.Load<Sprite>("Player/PlayerLeft");
            _right = Resources.Load<Sprite>("Player/PlayerRight");

            _movementSound = GetComponent<AudioSource>();

            // _renderer = GetComponent<SpriteRenderer>();
        }

        public void Update() {
            _verticalMove = Input.GetAxisRaw("Vertical");
            _horizontalMove = Input.GetAxisRaw("Horizontal");

            {
                if (Mathf.Abs(_verticalMove) <= 0.1f && Mathf.Abs(_horizontalMove) <= 0.1f) {
                    _movementSound.Stop();
                }
                else {
                    if (!_movementSound.isPlaying) {
                        _movementSound.Play();
                    }
                }
                
                if (_verticalMove > 0) {
                    _spriteRenderer.sprite = _back;
                }

                if (_verticalMove < 0) {
                    _spriteRenderer.sprite = _front;
                }
                
                if (_horizontalMove > 0) {
                    _spriteRenderer.sprite = _right;
                    // _spriteRenderer.flipX = false;
                }

                if (_horizontalMove < 0) {
                    _spriteRenderer.sprite = _left;
                    // _spriteRenderer.flipX = true;
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
