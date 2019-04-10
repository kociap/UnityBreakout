using System.Collections;
using System;
using UnityEngine;

public class TimeScale {
    private static Coroutine changeTimeScaleCoroutine = null;

    // Uses linear easing
    public static void ChangeTimeScale(float targetTimeScale, float changeDuration) {
        if (changeTimeScaleCoroutine != null) {
            Coroutine.Stop(changeTimeScaleCoroutine);
        }
        changeTimeScaleCoroutine = Coroutine.Start(_ChangeTimeScale(targetTimeScale, changeDuration, new Easing.Linear()));
    }

    public static void ChangeTimeScale(float targetTimeScale, float changeDuration, EasingFunction easing) {
        if (changeTimeScaleCoroutine != null) {
            Coroutine.Stop(changeTimeScaleCoroutine);
        }
        changeTimeScaleCoroutine = Coroutine.Start(_ChangeTimeScale(targetTimeScale, changeDuration, easing));
    }

    public static void ChangeImmediate(float value) {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // TODO remove? Why is it here?
    }

    //public static void ChangeAnimationSpeed(float targetSpeed, float changeDuration, Animation animation, string animationName) {
    //    Coroutine.Start(_ChangeAnimationSpeed(targetSpeed, changeDuration, animation, animationName, new Easing.Linear()));
    //}

    //public static void ChangeAnimationSpeed(float targetSpeed, float changeDuration, Animation animation, string animationName, EasingFunction easing) {
    //    Coroutine.Start(_ChangeAnimationSpeed(targetSpeed, changeDuration, animation, animationName, easing));
    //}

    private static IEnumerator _ChangeAnimationSpeed(float targetSpeed, float changeDuration, Animation animation, string animationName, EasingFunction easing) {
        //float timeSinceStart = 0f;
        //float initialSpeed = animation[animationName].speed;
        //if (initialSpeed > targetSpeed) {
        //    Compare = delegate (float a, float b) {
        //        return a > b;
        //    };
        //} else {
        //    Compare = delegate (float a, float b) {
        //        return a < b;
        //    };
        //}

        //while (Compare(animation[animationName].speed, targetSpeed)) {
        //    timeSinceStart += Time.deltaTime / timeScale;
        //    float percent = timeSinceStart / changeDuration;

        //    if (percent > 1) {
        //        percent = 1;
        //    }

        //    animation[animationName].speed = easing.Interpolate(initialSpeed, targetSpeed, timeSinceStart / changeDuration);
        //    yield return null;
        //}
        return null;
    }

    private static IEnumerator _ChangeTimeScale(float targetTimeScale, float duration, EasingFunction easing) {
        float progress = 0.0f;
        float initialTimeScale = Time.timeScale;
        while (progress < 1.0f) {
            progress = Mathf.Min(1.0f, progress + Time.unscaledDeltaTime / duration);
            float newTimeScale = easing.Interpolate(initialTimeScale, targetTimeScale, progress);
            ChangeImmediate(newTimeScale);
            yield return null;
        }

        yield break;
    }
}
