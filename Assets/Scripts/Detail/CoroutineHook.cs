using System;
using UnityEngine;

namespace Detail {
    public class CoroutineHook: MonoBehaviour {
        public static CoroutineHook CoroutineCaller { get; private set; }

        private void Awake() {
            if(CoroutineCaller != null) {
                Destroy(this);
                return;
            }

            CoroutineCaller = this;
            DontDestroyOnLoad(this);
        }
    }
}
