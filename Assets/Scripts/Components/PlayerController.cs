using UnityEngine;

public class PlayerController : MonoBehaviour {
    private new Animation animation;
    private Vector3 initialScale;
    [SerializeField] private AnimationClip wobbleAnimation;
    // Stretch factor. Bigger means bigger stretch, 0 means no stretch
    [SerializeField] private float stretchFactor = 0.5f;
    [SerializeField] private float translationSpeed = 18.0f;

    private void Start() {
        initialScale = gameObject.transform.localScale;
        animation = GetComponent<Animation>();
    }

    private void Update() {
        Vector3 currentPaddlePosition = gameObject.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        if (mousePosition.x != currentPaddlePosition.x) {
            Vector3 targetPosition = new Vector3(mousePosition.x, currentPaddlePosition.y, currentPaddlePosition.z);
            TranslatePaddle(currentPaddlePosition, targetPosition);
            ScalePaddle(currentPaddlePosition, targetPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            ReactToBallCollision();
        }
    }

    private void TranslatePaddle(Vector3 currentPaddlePosition, Vector3 targetPosition) {
        gameObject.transform.position = Vector3.Lerp(currentPaddlePosition, targetPosition, Time.deltaTime / TimeScale.timeScale * translationSpeed);
    }

    private void ScalePaddle(Vector3 currentPaddlePosition, Vector3 targetPosition) {
        float paddleScaleX = 1 + (targetPosition - currentPaddlePosition).magnitude * stretchFactor;
        float paddleScaleY = 1 / paddleScaleX;
        gameObject.transform.localScale = new Vector3(initialScale.x * paddleScaleX, initialScale.y * paddleScaleY, initialScale.z);
    }

    private void ReactToBallCollision() {
        animation.Stop();
        animation.Play(wobbleAnimation.name);
    }
}
