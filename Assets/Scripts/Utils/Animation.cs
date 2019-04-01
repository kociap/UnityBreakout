using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utils {
    public static class Animation {
        public static float GetClipLength(Animator animator, string clipName, int layer = 0) {
            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(layer);
            if(clips.Length == 0) { throw new Exception("No clips associated with animation"); }
            AnimationClip clip = clips[0].clip;
            return clip.length;
        }

        public static float GetStateLength(Animator animator, string stateName, int layer = 0) {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
            return info.length;
        }
    }
}
