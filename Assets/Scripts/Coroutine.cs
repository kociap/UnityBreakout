using System.Collections;

using static Detail.CoroutineHook;

public class Coroutine {
    public delegate IEnumerator CoroutineFunction();
    public delegate void CoroutineDefaultFunction();

    public static Coroutine Start(CoroutineFunction func) {
        return new Coroutine(CoroutineCaller.StartCoroutine(func()));
    }

    public static Coroutine Start(IEnumerator enumerator) {
        UnityEngine.Coroutine routine = CoroutineCaller.StartCoroutine(enumerator);
        if (routine == null) { UnityEngine.Debug.Log("Coroutine returned immediately"); }
        return new Coroutine(routine);
    }

    public static Coroutine Delay(float time, CoroutineDefaultFunction func) {
        UnityEngine.Coroutine routine = CoroutineCaller.StartCoroutine(DelayFunctionCall(time, func));
        if (routine == null) { UnityEngine.Debug.Log("Coroutine returned immediately"); }
        return new Coroutine(routine);
    }

    public static Coroutine DelayRealtime(float time, CoroutineDefaultFunction func) {
        UnityEngine.Coroutine routine = CoroutineCaller.StartCoroutine(DelayFunctionCallRealtime(time, func));
        if (routine == null) { UnityEngine.Debug.Log("Coroutine returned immediately"); }
        return new Coroutine(routine);
    }

    private static IEnumerator DelayFunctionCall(float time, CoroutineDefaultFunction func) {
        yield return new UnityEngine.WaitForSeconds(time);
        func();
    }

    private static IEnumerator DelayFunctionCallRealtime(float time, CoroutineDefaultFunction func) {
        yield return new UnityEngine.WaitForSecondsRealtime(time);
        func();
    }

    public static void Stop(Coroutine coroutine) {
        CoroutineCaller.StopCoroutine(coroutine.coroutine);
    }

    public static void StopAll() {
        CoroutineCaller.StopAllCoroutines();
    }

    private UnityEngine.Coroutine coroutine;

    private Coroutine(UnityEngine.Coroutine _coroutine) {
        coroutine = _coroutine;
    }
}
