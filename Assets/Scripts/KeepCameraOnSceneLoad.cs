using UnityEngine;

public class KeepCameraOnSceneLoad : MonoBehaviour {
    private static bool exists = false;

    private void Awake() {
        if (exists) {
            Destroy(gameObject);
        } else {
            exists = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}
