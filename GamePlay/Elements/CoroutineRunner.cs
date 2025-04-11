using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance = null;

    internal static CoroutineRunner Instance => instance != null ? instance : instance = new GameObject("Coroutine Runner").AddComponent<CoroutineRunner>();

    internal static void Run(IEnumerator coroutine)
    {
        if (instance == null)
            instance = new GameObject("Coroutine Runner").AddComponent<CoroutineRunner>();

        instance.StartCoroutine(coroutine);
    }

    internal static void DelayedAction(Action action, float delay)
    {
        if (instance == null)
            instance = new GameObject("Coroutine Runner").AddComponent<CoroutineRunner>();

        instance.StartCoroutine(instance.DoDelayedAction(action, delay));
    }

    private IEnumerator DoDelayedAction(Action action, float delay) 
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
