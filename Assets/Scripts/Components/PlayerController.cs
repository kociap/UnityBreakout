using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector3 defaultScale;
    private Vector2 velocity = Vector2.zero;

    // Stretch factor. Bigger means bigger stretch, 0 means no stretch
    public float stretchFactor = 0.04f;
    public float maxScaleX = 3.0f;
    public float minScaleDistance = 1.0f;
    public float translationSpeed = 1150.0f;
    public float smoothTime = 0.02f;
    [Header("Animations")]
    [SerializeField]
    private Animator animator;
    [SerializeField] private string wobbleName;

    private void Start() {
        defaultScale = transform.localScale;
    }

    private void Update() {
        Vector3 currentPaddlePosition = transform.position;
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
        // Moves with the same speed during slowmotion
        //transform.position = Vector3.Lerp(currentPaddlePosition, targetPosition, Time.unscaledDeltaTime * translationSpeed);
        transform.position = Vector2.SmoothDamp(currentPaddlePosition, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
    }

    private void ScalePaddle(Vector3 currentPaddlePosition, Vector3 targetPosition) {
        float distance = (targetPosition - currentPaddlePosition).magnitude;
        float distanceLimited = Mathf.Max(distance, minScaleDistance) - minScaleDistance;
        float paddleScaleX = Mathf.Min(1 + distanceLimited * stretchFactor, maxScaleX);
        float paddleScaleY = 1 / paddleScaleX;
        transform.localScale = new Vector3(defaultScale.x * paddleScaleX, defaultScale.y * paddleScaleY, defaultScale.z);
    }

    private void ReactToBallCollision() {
        animator.SetTrigger(wobbleName);
    }
}
