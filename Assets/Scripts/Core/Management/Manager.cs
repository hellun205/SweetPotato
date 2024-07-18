using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Management
{
  public static class Manager
  {
    private static GlobalManagementObject _globalManagementObject;
    public static GlobalManagementObject globalManagementObject {
      get {
        if (!isReady)
          throw new Exception("\"GlobalManagementObject\" has not been loaded yet.");
        else
          return _globalManagementObject;
      }
      set => _globalManagementObject = value;
    }

    public static Action onUpdate => globalManagementObject.onUpdate;

    public static Action onStart => globalManagementObject.onStart;

    public static Action onFixedUpdate => globalManagementObject.onFixedUpdate;

    public static Action onDestroy => globalManagementObject.onDestroy;

    private static bool _cacheIsLoaded = false;

    public static Queue<Action> initFuncQueue = new Queue<Action>();

    public static bool isReady { get; set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
      if (_cacheIsLoaded) return;

      new GameObject("[ShapeGMC]", typeof(GlobalManagementObject));
      _cacheIsLoaded = true;
    }

    public static Coroutine StartCoroutine(IEnumerator routineFunction) => globalManagementObject.StartCoroutine(routineFunction);

    public static void StopCoroutine(Coroutine coroutine) => globalManagementObject.StopCoroutine(coroutine);

    public static void CallInit(Action func)
    {
      if (isReady)
        func?.Invoke();
      else
        initFuncQueue.Enqueue(func);
    }
  }
}
