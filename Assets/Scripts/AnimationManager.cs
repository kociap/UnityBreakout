using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager {
    public delegate void PropertyAnimator(float value);
    public delegate void Callback();
    private delegate bool FloatComparator(float a, float b);

    public static Coroutine Animate(float initial, float target, float duration, EasingFunction easing, PropertyAnimator propertyAnimator, Callback callback = null) {
        return Coroutine.Start(_Animate(initial, target, duration, easing, propertyAnimator, callback));
    }

    private static IEnumerator _Animate(float initial, float target, float duration, EasingFunction easing, PropertyAnimator propertyAnimator, Callback callback) {
        float current = initial;
        float progress = 0.0f;
        while (progress < 1.0f) {
            progress = Mathf.Min(1.0f, progress + Time.unscaledDeltaTime / duration);
            current = easing.Interpolate(initial, target, progress);
            propertyAnimator(current);
            yield return null;
        }

        if (callback != null) {
            callback();
        }
    }

    public static Coroutine PlayAnimationDelayed(Animator animator, string stateName, float delay, Callback callback = null) {
        return Coroutine.Start(_PlayAnimationDelayed(animator, stateName, delay, callback));
    }

    private static IEnumerator _PlayAnimationDelayed(Animator animator, string stateName, float delay, Callback callback) {
        yield return new WaitForSeconds(delay);
        animator.Play(stateName);

        if(callback != null) {
            float time = Utils.Animation.GetStateLength(animator, stateName);
            yield return new WaitForSeconds(time);
            callback();
        }
    }

    public static Coroutine PlayAnimationDelayedRealtime(Animator animator, string stateName, float delay, Callback callback = null) {
        return Coroutine.Start(_PlayAnimationDelayedRealtime(animator, stateName, delay, callback));
    }

    private static IEnumerator _PlayAnimationDelayedRealtime(Animator animator, string stateName, float delay, Callback callback) {
        yield return new WaitForSecondsRealtime(delay);
        animator.Play(stateName);

        if(callback != null) {
            float time = Utils.Animation.GetStateLength(animator, stateName);
            yield return new WaitForSecondsRealtime(time);
            callback();
        }
    }
}
