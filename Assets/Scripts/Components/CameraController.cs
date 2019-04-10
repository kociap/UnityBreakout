using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BackgroundColorGlitch))]
[RequireComponent(typeof(CameraShake))]
public class CameraController : MonoBehaviour {
    private BackgroundColorGlitch bgGlitch;
    private CameraShake shake;

    private void Start() {
        bgGlitch = GetComponent<BackgroundColorGlitch>();
        shake = GetComponent<CameraShake>();
    }

    public void GlitchBackgroundColor() {
        bgGlitch.GlitchBackgroundColor();
    }

    public void Shake() {
        shake.Shake();
    }

}
