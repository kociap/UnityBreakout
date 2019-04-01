using UnityEngine; 

public class BallController: MonoBehaviour {
    private new Rigidbody2D rigidbody;
    private new Animation animation;
    [SerializeField] private AnimationClip wobbleAnimation;
    [SerializeField] private GameObject particle;
    [SerializeField] private int numberOfParticles = 4;
    [SerializeField] private float particleSpeed = 6.0f;
    [SerializeField] private float particleLifetime = 0.4f;
    [SerializeField] private float speed = 10;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.up * speed;
        animation = GetComponent<Animation>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.Shake();
        Wobble();

        if (collision.gameObject.CompareTag("Block")) {
            cameraController.GlitchBackgroundColor();
        }

        if (!collision.gameObject.CompareTag("Player")) {
            ChangeVelocity(rigidbody.velocity.normalized * speed);
        } else {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 paddlePos = collision.gameObject.transform.position;
            Vector2 bounceAdditionalVelocity = new Vector2(contactPoint.x - paddlePos.x, contactPoint.y - paddlePos.y).normalized;
            float ballSpeed = rigidbody.velocity.magnitude;
            Vector2 newVelocity = (bounceAdditionalVelocity * ballSpeed / 2 + rigidbody.velocity).normalized * ballSpeed;
            ChangeVelocity(newVelocity);
        }

        for (int i = 0; i < numberOfParticles; ++i) {
            Vector2 position = collision.contacts[0].point;
            Vector2 velocity = Utils.Math.RotateVector(rigidbody.velocity, Random.Range(-40f, 40f)).normalized;
            EmitParticle(particleLifetime, position, velocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameController.PlayerLost();
    }

    public void ChangeVelocity(Vector2 velocity) {
        transform.rotation = Quaternion.Euler(0, 0, Utils.Math.AngleFromVector(velocity));
        rigidbody.velocity = velocity;
    }

    private void Wobble() {
        animation.Stop();
        animation.Play(wobbleAnimation.name);
    }

    private void EmitParticle(float lifetime, Vector2 position, Vector2 velocity) {
        ParticleController particleController = ParticleController.InstantiateParticle(particle, lifetime, position, velocity);
    }
}
