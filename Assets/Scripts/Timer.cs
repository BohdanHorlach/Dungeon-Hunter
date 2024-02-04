using System.Collections;
using UnityEngine;


public static class Timer
{
    class MonoBehaviourHook : MonoBehaviour { }

    private static MonoBehaviour stub;

    static Timer()
    {
        stub = new GameObject("Timer").AddComponent<MonoBehaviourHook>();
    }


    private static IEnumerator RunTimer(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);

        if(callback != null)
            callback();
    }


    public static void StartTimer(float time, System.Action action)
    {
        stub.StartCoroutine(RunTimer(time, action));
    }

}