using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Core.Management
{
  public static class AssetMgr
  {
    private static HashSet<Object> loadedAssets = new HashSet<Object>();
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init() =>
      Manager.CallInit(() => {
        Manager.StartCoroutine(InitAddressableRoutine());
      });

    private static IEnumerator InitAddressableRoutine()
    {
      var req = Addressables.InitializeAsync();
      yield return req;
    }
    
    private static IEnumerator LoadRoutine<T>(AssetReference assetRef, Action<T> callback) where T : Object
    {
      if (assetRef.IsValid())
        callback?.Invoke((T)assetRef.Asset);
      else
      {
        var req = assetRef.LoadAssetAsync<T>();
        yield return req;
        callback?.Invoke(req.Result);
      }
    }

    public static void Load(this AssetReference assetRef, Action<Object> callback = null) 
      => Manager.StartCoroutine(LoadRoutine(assetRef, callback));
    
    public static void Load<T>(this AssetReference assetRef, Action<T> callback) where T : Object
      => Manager.StartCoroutine(LoadRoutine(assetRef, callback));
  }
}
