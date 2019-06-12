using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionController : Utils.StaticMonobehaviour<SlowMotionController> {
    public float slowMotionTimeScale = 0.5f;
    public float defaultTimeScale = 1f;
    public float slowMotionTimeScaleChangeDuration = 0.5f;

    public static float timeLeft = 50000.0f;
    public static bool inSlowMotion = false;

	private void Update () {
        if(inSlowMotion) {
            timeLeft -= Time.unscaledDeltaTime;
        }

        if (timeLeft <= 0 || Input.GetMouseButtonUp(1)) {
            TimeScale.ChangeTimeScale(defaultTimeScale, slowMotionTimeScaleChangeDuration);
            timeLeft = Mathf.Max(0.0f, timeLeft);
            inSlowMotion = false;
        } else if (!GameController.loadingScene && timeLeft >= slowMotionTimeScaleChangeDuration && Input.GetMouseButtonDown(1)) {
            TimeScale.ChangeTimeScale(slowMotionTimeScale, slowMotionTimeScaleChangeDuration);
            inSlowMotion = true;
        }
	}
}
