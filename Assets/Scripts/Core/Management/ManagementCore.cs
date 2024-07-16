using System;
using System.Collections;
using UnityEngine;

namespace Core.Management
{
  public static class ManagementCore
  {
    public static GlobalManagementObject globalManagementObject { get; private set; }

    public static class Events
    {
      public static Action onUpdate => globalManagementObject.onUpdate;

      public static Action onStart => globalManagementObject.onStart;

      public static Action onFixedUpdate => globalManagementObject.onFixedUpdate;

      public static Action onDestroy => globalManagementObject.onDestroy;
    }

    private static bool _cacheIsLoaded = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
      if (_cacheIsLoaded) return;

      globalManagementObject = new GameObject("[ShapeGMC]", typeof(GlobalManagementObject)).GetComponent<GlobalManagementObject>();

      _cacheIsLoaded = true;
    }

    public static Coroutine StartCoroutine(IEnumerator routineFunction)
    {
      return globalManagementObject.StartCoroutine(routineFunction);
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
      globalManagementObject.StopCoroutine(coroutine);
    }
  }
}
