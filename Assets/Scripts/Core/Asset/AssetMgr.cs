using System;
using System.Collections;
using Core.Management;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Core.Asset
{
  public static class AssetMgr
  {

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init() =>
      Manager.CallInit(() => { Manager.StartCoroutine(InitAddressableRoutine()); });

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

    public static void Load<T>(this AssetReference assetRef, Action<T> callback = null) where T : Object
      => Manager.StartCoroutine(LoadRoutine(assetRef, callback));

    private static IEnumerator LoadAssetRoutine<T>(string assetAddress, Action<T> callback) where T : Object
    {
      var req = Addressables.LoadAssetAsync<T>(assetAddress);
      yield return req;
      callback?.Invoke(req.Result);
    }

    public static void LoadAsset(this string assetAddress, Action<Object> callback = null)
      => Manager.StartCoroutine(LoadAssetRoutine(assetAddress, callback));

    public static void LoadAsset<T>(this string assetAddress, Action<T> callback = null) where T : Object
      => Manager.StartCoroutine(LoadAssetRoutine(assetAddress, callback));
  }
}
