using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController: Utils.StaticMonobehaviour<ImageController> {
    [Range(0, 1)]
    public float saturation = 1f;
    public float blurDuration = 0.5f;
    public Material postEffectShader;
    public string saturationKeyword = "_Saturation";

    private Material shader;
    private static Coroutine saturationCoroutine = null;

    private void Start() {
        shader = new Material(postEffectShader);
        shader.EnableKeyword(saturationKeyword);
    }

    public static void BlurScreen(int pixels) {

    }

    public static void DesaturateScreen(float time) {
        if(saturationCoroutine != null) {
            Coroutine.Stop(saturationCoroutine);
        }
        saturationCoroutine = AnimationManager.Animate(1.0f, 0.0f, time, new Easing.QuarticOut(), SetSaturation);
    }

    public static void SaturateScreen(float time) {
        if (saturationCoroutine != null) {
            Coroutine.Stop(saturationCoroutine);
        }
        saturationCoroutine = AnimationManager.Animate(0.0f, 1.0f, time, new Easing.QuarticOut(), SetSaturation);
    }

    public static void SetSaturation(float value) {
        v.shader.SetFloat(v.saturationKeyword, value);
        Debug.Log("_Saturation " + v.shader.GetFloat(v.saturationKeyword));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, shader);
    }
}
