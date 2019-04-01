using System;
using UnityEngine;

namespace Utils {
    public static class Math {
        public static Vector2 RotateVector(Vector2 vector, float degrees) {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
            float x = vector.x;
            float y = vector.y;
            vector.x = x * cos - sin * y;
            vector.y = x * sin + cos * y;
            return vector;
        }

        public static float AngleFromVector(Vector2 vector) {
            return Mathf.Atan2(vector.y, vector.x) * 180 / Mathf.PI;
        }
    }
}
