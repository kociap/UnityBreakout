using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager {
    public delegate void PropertyAnimator(float value);
    public delegate void Callback();
    private delegate bool FloatComparator(float a, float b);

    public static void Animate(float initial, float target, float duration, EasingFunction easing, PropertyAnimator propertyAnimator, Callback callback = null) {
        Coroutine.Start(_Animate(initial, target, duration, easing, propertyAnimator, callback));
    }

    private static IEnumerator _Animate(float initial, float target, float duration, EasingFunction easing, PropertyAnimator propertyAnimator, Callback callback) {
        float timeSinceStart = 0f;
        float current = initial;
        FloatComparator Compare;
        if (initial > target) {
            Compare = delegate (float a, float b) {
                return a > b;
            };
        } else {
            Compare = delegate (float a, float b) {
                return a < b;
            };
        }

        while (Compare(current, target)) {
            timeSinceStart += Time.deltaTime / Time.timeScale;
            float percent = timeSinceStart / duration;

            if (percent > 1) {
                percent = 1;
            }

            current = easing.Interpolate(initial, target, percent);
            propertyAnimator(current);
            yield return null;
        }

        if (callback != null) {
            callback();
        }
    }

    public static void PlayAnimationDelayed(Animator animator, string stateName, float delay, Callback callback = null) {
        Coroutine.Start(_PlayAnimationDelayed(animator, stateName, delay, callback));
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

    public static void PlayAnimationDelayedRealtime(Animator animator, string stateName, float delay, Callback callback = null) {
        Coroutine.Start(_PlayAnimationDelayedRealtime(animator, stateName, delay, callback));
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
