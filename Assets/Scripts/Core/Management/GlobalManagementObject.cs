using System;
using UnityEngine;

namespace Core.Management
{
  public class GlobalManagementObject : MonoBehaviour
  {
    public Action onStart;
    public Action onFixedUpdate;
    public Action onDestroy;
    public Action onUpdate;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
      Debug.LogError("Shape GMC object has been destroyed. The system may not work properly.");
#endif
      onDestroy?.Invoke();
    }

    private void Start() => onStart?.Invoke();

    private void Update() => onUpdate?.Invoke();

    private void FixedUpdate() => onFixedUpdate?.Invoke();


  }
}
