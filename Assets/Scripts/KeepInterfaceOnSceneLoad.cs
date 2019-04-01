using UnityEngine;

public class KeepInterfaceOnSceneLoad : MonoBehaviour {
    private static KeepInterfaceOnSceneLoad _instance = null;

	private void Awake () {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
	}
}
