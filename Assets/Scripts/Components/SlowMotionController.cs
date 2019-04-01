using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionController : MonoBehaviour {
    public float slowMotionTimeScale = 1f;
    public float defaultTimeScale = 1f;
    public float slowMotionTimeScaleChangeDuration = 0.5f;
    public AnimationClip paddleWobbleAnimation;

	private void Update () {
        if (Input.GetMouseButtonDown(1)) {
            TimeScale.ChangeTimeScale(slowMotionTimeScale, slowMotionTimeScaleChangeDuration);
        } else if (Input.GetMouseButtonUp(1)) {
            TimeScale.ChangeTimeScale(defaultTimeScale, slowMotionTimeScaleChangeDuration);
        }
	}
}
