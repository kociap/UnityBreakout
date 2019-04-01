using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartInitialization : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnGameInitialization() {
        SceneManager.LoadScene(Scenes.ui);
        SceneManager.LoadScene(Scenes.main_menu);
    }
}
