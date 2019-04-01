using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController: MonoBehaviour {
    [Range(0, 1)]
    public float saturation = 1f;
    public float desaturationDuration = 0.5f;
    public float blurDuration = 0.5f;
    public Material postEffectShader;
    public string saturationKeyword = "_Saturation";

    private Material shader;
    private static ImageController v = null;

    private void Update() {
        shader.SetFloat(saturationKeyword, saturation);
    }

    private void Awake() {
        if(v != null) {
            Destroy(this);
            return;
        }

        v = this;
    }

    private void Start() {
        shader = new Material(postEffectShader);
    }

    public static void BlurScreen(int pixels) {

    }

    public static void DesaturateScreen() {
        AnimationManager.PropertyAnimator propertyAnimator = (float value) => {
            v.shader.EnableKeyword(v.saturationKeyword);
            v.shader.SetFloat(v.saturationKeyword, value);
            v.shader.DisableKeyword(v.saturationKeyword);
        };

        AnimationManager.Callback callback = () => {
            v.shader.EnableKeyword(v.saturationKeyword);
            v.shader.SetFloat(v.saturationKeyword, 1.0f);
            v.shader.DisableKeyword(v.saturationKeyword);
        };

        AnimationManager.Animate(1.0f, 0.0f, v.desaturationDuration, new Easing.QuarticOut(), propertyAnimator, callback);
    }

    public static void SetSaturation(float saturation) {
        v.shader.EnableKeyword(v.saturationKeyword);
        v.shader.SetFloat(v.saturationKeyword, saturation);
        v.shader.DisableKeyword(v.saturationKeyword);
        Debug.Log("_Saturation " + v.shader.GetFloat(v.saturationKeyword));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, shader);
    }
}
