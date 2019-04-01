using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour {
    public AnimationClip gameOverAnimation;
    public AnimationClip loadingAnimation;
    public AnimationClip loadingEndAnimaiton;
    public GameObject gameOverElement;
    public GameObject loadingElement;
    public string gameOverStateName;
    public string levelLoadStateName;
    public string levelLoadEndStateName;
    private float loadingDelay = 0f;
    private delegate void EventCallback();
    private static event EventCallback OnSceneLoaded;

    private static SceneLoader v = null;

    private void Awake() {
        if(v != null) {
            Destroy(this);
            return;
        }

        v = this;
    }

    private void Start() {
        loadingDelay = gameOverAnimation.length - loadingAnimation.length;
    }

    public static void LoadScene(string sceneName) {
        ImageController.DesaturateScreen();
        v.gameOverElement.GetComponent<Animator>().Play(v.gameOverStateName);
        AnimationManager.PlayAnimationDelayedRealtime(v.loadingElement.GetComponent<Animator>(), v.levelLoadStateName, v.loadingDelay, () => {
            OnSceneLoaded += PrepareNewScene;

            Coroutine.Start(LoadSceneAsync(sceneName));
        });
    }

    public static void ReloadCurrentScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        LoadScene(currentSceneName);
    }

    private static IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);
        while (sceneLoading.isDone) {
            yield return null;
        }

        if (OnSceneLoaded != null) {
            OnSceneLoaded();
        }
    }

    private static void PrepareNewScene() {
        OnSceneLoaded -= PrepareNewScene;
        Animator animator = v.loadingElement.GetComponent<Animator>();
        animator.Play(v.levelLoadEndStateName);
        float time = Utils.Animation.GetStateLength(animator, v.levelLoadEndStateName);
        Coroutine.Delay(time, () => {
            Debug.Log("Scene loading done");
        });
    }
}
