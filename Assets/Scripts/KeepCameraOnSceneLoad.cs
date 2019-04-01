using UnityEngine;

public class KeepCameraOnSceneLoad : MonoBehaviour {
    private static KeepCameraOnSceneLoad _instance = null;

    private void Awake() {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }
}
