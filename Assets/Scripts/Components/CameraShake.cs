using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour {
    public float shakeIntensity = 0.04f;
    // Duration in milliseconds
    public float shakeDuration = 0.15f;
    private bool shaking = false;
    private Vector3 initialPosition;

    void Start () {
        initialPosition = transform.localPosition;
	}

    public void Shake() {
        if (!shaking) {
            shaking = true;
            Coroutine.Start(_Shake());
        }
    }

    private IEnumerator _Shake() {
        float shakeTime = shakeDuration;
        while (shakeTime > 0f) {
            shakeTime -= Time.unscaledDeltaTime;
            gameObject.transform.localPosition = initialPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
            yield return null;
        }

        gameObject.transform.localPosition = initialPosition;
        shaking = false;
    }
}
