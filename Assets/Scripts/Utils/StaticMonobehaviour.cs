using System;
using UnityEngine;

namespace Utils {
    public class StaticMonobehaviour<T>: MonoBehaviour where T: StaticMonobehaviour<T> {
        public static T v;
        private static bool created = false;

        private void Awake() {
            if(created) {
                Destroy(gameObject);
                return;
            }

            v = (T)this;
            created = true;
        }
    }
}
