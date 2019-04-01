using System;
using UnityEngine;

public interface EasingFunction {
    Vector2 Interpolate(Vector2 beg, Vector2 end, float percent);
    float Interpolate(float beg, float end, float percent);
}

namespace Easing {
    public class Linear: EasingFunction {
        public Vector2 Interpolate(Vector2 beg, Vector2 end, float percent) {
            return beg + (end - beg) * percent;
        }

        public float Interpolate(float beg, float end, float percent) {
            return beg + (end - beg) * percent;
        }
    }

    public class QuarticIn: EasingFunction {
        public Vector2 Interpolate(Vector2 beg, Vector2 end, float percent) {
            return beg + (end - beg) * (Mathf.Pow(percent, 4));
        }

        public float Interpolate(float beg, float end, float percent) {
            return beg + (end - beg) * (Mathf.Pow(percent, 4));
        }
    }

    public class QuadraticOut: EasingFunction {
        public Vector2 Interpolate(Vector2 beg, Vector2 end, float percent) {
            return beg + (end - beg) * (-Mathf.Pow(percent - 1, 2) + 1);
        }

        public float Interpolate(float beg, float end, float percent) {
            return beg + (end - beg) * (-Mathf.Pow(percent - 1, 2) + 1);
        }
    }

    public class CubicOut: EasingFunction {
        public Vector2 Interpolate(Vector2 beg, Vector2 end, float percent) {
            return beg + (end - beg) * (Mathf.Pow(percent - 1, 3) + 1);
        }

        public float Interpolate(float beg, float end, float percent) {
            return beg + (end - beg) * (Mathf.Pow(percent - 1, 3) + 1);
        }
    }

    public class QuarticOut: EasingFunction {
        public Vector2 Interpolate(Vector2 beg, Vector2 end, float percent) {
            return beg + (end - beg) * (-Mathf.Pow(percent - 1, 4) + 1);
        }

        public float Interpolate(float beg, float end, float percent) {
            return beg + (end - beg) * (-Mathf.Pow(percent - 1, 4) + 1);
        }
    }
}
