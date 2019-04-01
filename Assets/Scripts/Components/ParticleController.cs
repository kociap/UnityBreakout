using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ParticleController : MonoBehaviour {
    public static ParticleController InstantiateParticle(GameObject prefab, float lifetime, Vector2 position, Vector2 velocity) {
        GameObject instantiatedObject = Instantiate(prefab);
        ParticleController particle = instantiatedObject.GetComponent<ParticleController>();
        particle.rigidbody = instantiatedObject.GetComponent<Rigidbody2D>();
        particle.rigidbody.velocity = particle.initialVelocity = velocity;
        particle.transform.position = position;
        particle.lifetime = lifetime;
        return particle;
    }

    public static ParticleController InstantiateParticle(GameObject prefab, float lifetime, Vector2 position, Vector2 velocity, EasingFunction easing) {
        ParticleController particle = InstantiateParticle(prefab, lifetime, position, velocity);
        particle.easing = easing;
        return particle;
    }

    private float lifetime;
    private Vector2 initialVelocity;
    private new Rigidbody2D rigidbody;
    private EasingFunction easing = new Easing.QuarticIn();
    private Vector3 initialScale;

    private void Start() {
        initialScale = transform.localScale;
    }

    private void Update() {
        if (lifetime <= 0) {
            Destroy(gameObject);
            return;
        }

        lifetime -= Time.deltaTime;
        rigidbody.velocity = easing.Interpolate(initialVelocity, new Vector2(0, 0), 1 - lifetime);
        transform.localScale = easing.Interpolate(initialScale, new Vector3(0, 0, 0), 1 - lifetime);
    }
}
