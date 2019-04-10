using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BackgroundColorGlitch : MonoBehaviour {
    public int colorChanges = 3;
    public int changeFrameInterval = 2;

    [Header("Background color (HSL)")]
    // Default color hsl(0, 92%, 86%)
    public float backgroundHue = 90f / 360f;
    public float backgroundSaturation = 0.92f;
    public float backgroundLightness = 0.86f;

    private new Camera camera;
    private bool glitching = false;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    public void GlitchBackgroundColor() {
        if (!glitching) {
            glitching = true;
            Coroutine.Start(_GlitchBackgroundColor());
        }
    }

    private IEnumerator _GlitchBackgroundColor() {
        int changes = 0;
        int framesSinceLastColorChange = 0;
        while (changes < colorChanges) {
            ++framesSinceLastColorChange;
            if (framesSinceLastColorChange >= changeFrameInterval) {
                float randomBackgroundHue = (backgroundHue + Random.Range(0f, 1f)) % 1f;
                Color newBackgroundColor = Color.HSVToRGB(randomBackgroundHue, backgroundSaturation, backgroundLightness);
                camera.backgroundColor = newBackgroundColor;

                ++changes;
                framesSinceLastColorChange = 0;
            }

            yield return null;
        }
        camera.backgroundColor = Color.HSVToRGB(backgroundHue, backgroundSaturation, backgroundLightness);

        glitching = false;
    }
}
