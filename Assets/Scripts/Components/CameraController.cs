using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float desaturationDuration = 0.5f;
    public float shakeIntensity = 0.04f;
    // Duration in milliseconds
    public float shakeDuration = 0.15f;

    private float _currentShakeDuration;
    private Vector3 _initialPosition;
    private Camera _camera;
    private bool _screenGlitchRunning = false;

    // Default color hsl(0, 92%, 86%)
    private const float _backgroundSaturation = 0.92f;
    private const float _backgroundLightness = 0.86f;
    private float _backgroundHue = 90f / 360f;

    private void Start() {
        _initialPosition = gameObject.transform.position;
        _camera = gameObject.GetComponent<Camera>();
    }

    public void Shake() {
        bool shouldCallCoroutine = _currentShakeDuration <= 0f;
        _currentShakeDuration = shakeDuration;

        if (shouldCallCoroutine) {
            StartCoroutine(_Shake());
        }
    }

    public void GlitchBackgroundColor() {
        if (_screenGlitchRunning) {
            return;
        }

        StartCoroutine(_GlitchBackgroundColor());
    }

    private IEnumerator _Shake() {
        while (_currentShakeDuration > 0f) {
            _currentShakeDuration -= Time.deltaTime;
            gameObject.transform.position = _initialPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
            yield return null;
        }

        gameObject.transform.position = _initialPosition;
    }

    private IEnumerator _GlitchBackgroundColor() {
        _screenGlitchRunning = true;
        int colorChanges = 0;
        int framesSinceLastColorChange = 0;

        while (colorChanges < 3) {
            ++framesSinceLastColorChange;
            if (framesSinceLastColorChange >= 2) {
                float randomBackgroundHue = (_backgroundHue + Random.Range(0f, 1f)) % 1f;
                Color newBackgroundColor = Color.HSVToRGB(randomBackgroundHue, _backgroundSaturation, _backgroundLightness);
                _camera.backgroundColor = newBackgroundColor;

                ++colorChanges;
                framesSinceLastColorChange = 0;
            }

            yield return null;
        }
        _camera.backgroundColor = Color.HSVToRGB(_backgroundHue, _backgroundSaturation, _backgroundLightness);

        _screenGlitchRunning = false;
    }
}
