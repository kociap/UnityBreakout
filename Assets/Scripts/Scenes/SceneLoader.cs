using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: Utils.StaticMonobehaviour<SceneLoader> {
    public GameObject loadingElement;
    public string levelLoadName;
    public string levelLoadEndName;
    [Header("Screen Effects")]
    public float desaturationLength = 1.0f;
    public float loadDelay = 0.5f;

    private delegate void EventCallback();
    private static event EventCallback OnSceneLoaded;

    private void Start() {
        OnSceneLoaded += PrepareNewScene;
    }

    public static void LoadScene(string sceneName) {
        GameController.inputEnabled = false;
        GameController.loadingScene = true;
        Animator animator = v.loadingElement.GetComponent<Animator>();
        animator.SetTrigger(v.levelLoadName);
        float stateDuration = Utils.Animation.GetStateLength(animator, v.levelLoadName);
        Coroutine.DelayRealtime(stateDuration, () => {
            Coroutine.Start(LoadSceneAsync(sceneName));
        });
    }

    public static void ReloadCurrentScene() {
        GameController.inputEnabled = false;
        GameController.loadingScene = true;
        ImageController.DesaturateScreen(v.desaturationLength);
        TimeScale.ChangeTimeScale(0, v.desaturationLength);
        Coroutine.DelayRealtime(v.desaturationLength + v.desaturationLength, () => {
            string currentSceneName = SceneManager.GetActiveScene().name;
            LoadScene(currentSceneName);
        });
    }

    private static IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);
        while (sceneLoading.isDone) {
            yield return null;
        }

        if (OnSceneLoaded != null) {
            OnSceneLoaded();
        }

        yield break;
    }

    private static void PrepareNewScene() {
        TimeScale.ChangeImmediate(1.0f);
        ImageController.SetSaturation(1.0f);
        Animator animator = v.loadingElement.GetComponent<Animator>();
        animator.SetTrigger(v.levelLoadEndName);
        float time = Utils.Animation.GetStateLength(animator, v.levelLoadEndName);
        Coroutine.Delay(time, () => {
            GameController.inputEnabled = true;
            GameController.loadingScene = false;
            Debug.Log("Scene loading done");
        });
    }
}
