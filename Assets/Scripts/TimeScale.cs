using System.Collections;
using System;
using UnityEngine;

public class TimeScale {
    public static float timeScale { get; private set; } = 1;

    private static Coroutine changeTimeScaleCoroutine = null;
    private delegate bool FloatComparator(float a, float b);

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

    //public static void ChangeAnimationSpeed(float targetSpeed, float changeDuration, Animation animation, string animationName) {
    //    Coroutine.Start(_ChangeAnimationSpeed(targetSpeed, changeDuration, animation, animationName, new Easing.Linear()));
    //}

    //public static void ChangeAnimationSpeed(float targetSpeed, float changeDuration, Animation animation, string animationName, EasingFunction easing) {
    //    Coroutine.Start(_ChangeAnimationSpeed(targetSpeed, changeDuration, animation, animationName, easing));
    //}

    private static IEnumerator _ChangeAnimationSpeed(float targetSpeed, float changeDuration, Animation animation, string animationName, EasingFunction easing) {
        float timeSinceStart = 0f;
        float initialSpeed = animation[animationName].speed;
        FloatComparator Compare;
        if (initialSpeed > targetSpeed) {
            Compare = delegate (float a, float b) {
                return a > b;
            };
        } else {
            Compare = delegate (float a, float b) {
                return a < b;
            };
        }

        while (Compare(animation[animationName].speed, targetSpeed)) {
            timeSinceStart += Time.deltaTime / timeScale;
            float percent = timeSinceStart / changeDuration;

            if (percent > 1) {
                percent = 1;
            }

            animation[animationName].speed = easing.Interpolate(initialSpeed, targetSpeed, timeSinceStart / changeDuration);
            yield return null;
        }
    }

    private static IEnumerator _ChangeTimeScale(float targetTimeScale, float changeDuration, EasingFunction easing) {
        float timeSinceStart = 0f;
        float initialTimeScale = timeScale;
        FloatComparator Compare;
        if (initialTimeScale > targetTimeScale) {
            Compare = delegate (float a, float b) { return a > b; };
        } else {
            Compare = delegate (float a, float b) { return a < b; };
        }

        while (Compare(timeScale, targetTimeScale)) {
            timeSinceStart += Time.deltaTime / timeScale;
            float percent = timeSinceStart / changeDuration;

            if (percent > 1) {
                percent = 1;
            }

            Time.timeScale = timeScale = easing.Interpolate(initialTimeScale, targetTimeScale, timeSinceStart / changeDuration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }
    }
}
