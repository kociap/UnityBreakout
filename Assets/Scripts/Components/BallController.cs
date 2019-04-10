using UnityEngine; 

public class BallController: MonoBehaviour {
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private CameraController cameraController;
    private int horizontalWallBounces = 0;

    [SerializeField] private float speed = 10;
    [SerializeField] private float verticalThresholdSpeed = 0.3f;
    [SerializeField] private int maxHorizontalBounces = 4;
    [SerializeField] private float unstuckVelocity = 0.5f;
    [Header("Particles")]
    [SerializeField] private GameObject particle;
    [SerializeField] private int numberOfParticles = 4;
    [SerializeField] private float particleSpeed = 6.0f;
    [SerializeField] private float particleLifetime = 0.4f;
    [Header("Animations")]
    [SerializeField] private string wobbleName;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update() {
        bool stationary = rigidbody.velocity.sqrMagnitude <= 0;
        bool mouseButtonReleased = Input.GetMouseButtonUp(0);
        if (GameController.inputEnabled && mouseButtonReleased && stationary) {
            rigidbody.velocity = Vector2.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        cameraController.Shake();
        Wobble();

        if (collision.gameObject.CompareTag("Block")) {
            cameraController.GlitchBackgroundColor();
        }

        // Horizontal unstucking
        if(collision.gameObject.CompareTag("Wall")) {
            if(Mathf.Abs(rigidbody.velocity.y) < verticalThresholdSpeed) {
                ++horizontalWallBounces;
            }

            if(horizontalWallBounces == maxHorizontalBounces) {
                horizontalWallBounces = 0;
                Vector2 v = rigidbody.velocity;
                float vHorizontal = Mathf.Sign(v.x) * Mathf.Sqrt(speed * speed - unstuckVelocity * unstuckVelocity);
                rigidbody.velocity = new Vector2(vHorizontal, unstuckVelocity);
            }
        }

        if (collision.gameObject.CompareTag("Player")) {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 paddlePos = collision.gameObject.transform.position;
            Vector2 bounceAdditionalVelocity = new Vector2(contactPoint.x - paddlePos.x, contactPoint.y - paddlePos.y).normalized;
            Vector2 newVelocity = (bounceAdditionalVelocity * speed / 2 + rigidbody.velocity).normalized * speed;
            rigidbody.velocity = newVelocity;
        }

        UpdateOrientation(rigidbody.velocity);

        for (int i = 0; i < numberOfParticles; ++i) {
            Vector2 position = collision.contacts[0].point;
            Vector2 velocity = Utils.Math.RotateVector(rigidbody.velocity, Random.Range(-40f, 40f)).normalized;
            EmitParticle(particleLifetime, position, velocity * particleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("powerup")) {
            Powerup powerup = collision.gameObject.GetComponent<Powerup>();
            Powerups.PowerupCollected(powerup);
            Destroy(collision.gameObject);
        } else {
            GameController.PlayerLost();
        }
    }

    private void UpdateOrientation(Vector2 velocity) {
        transform.rotation = Quaternion.Euler(0, 0, Utils.Math.AngleFromVector(velocity));
    }

    private void Wobble() {
        animator.SetTrigger(wobbleName);
    }

    private void EmitParticle(float lifetime, Vector2 position, Vector2 velocity) {
        ParticleController particleController = ParticleController.InstantiateParticle(particle, lifetime, position, velocity);
    }
}
