using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Management;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Core.Asset.Entity
{
  public static class EntityMgr
  {
    public static Transform entityParent;
    public static Transform uiEntityParent;
    private static Vector2 _tmpPos;

    private static Dictionary<string, Utilities.ObjectPool<Entity>> _pools = new Dictionary<string, Utilities.ObjectPool<Entity>>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
      Manager.CallInit(() => {
        SceneManager.activeSceneChanged += (_, _) => OnActiveSceneChanged();
        OnActiveSceneChanged();
      });
    }

    private static void OnActiveSceneChanged()
    {
      foreach (var (name, pool) in _pools)
        pool.Clear();
      _pools.Clear();

      if (entityParent == null)
      {
        entityParent = new GameObject("[Entities]").transform;
      }

      if (uiEntityParent == null)
      {
        uiEntityParent = new GameObject("[Entities(UI)]", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)).transform;
        var cv = uiEntityParent.GetComponent<Canvas>();
        var rt = uiEntityParent.GetComponent<RectTransform>();
        cv.renderMode = RenderMode.WorldSpace;
        cv.vertexColorAlwaysGammaSpace = true;
        rt.localPosition = Vector3.zero;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.zero;
        rt.sizeDelta = new Vector2(3000f, 3000f);
        rt.localScale = new Vector3(0.05f, 0.05f, 1f);
      }
    }

    public static async Task<Entity> Summon(string assetAddress, Vector2 position)
    {
      if (!_pools.ContainsKey(assetAddress))
      {
        _pools.Add(assetAddress, new Utilities.ObjectPool<Entity>(() => CreateFunc(assetAddress), ActionOnGet, ActionOnRelease, ActionOnDestroy));
      }
      return await _pools[assetAddress].Get();
    }

    public static void Kill(Entity entity)
    {
      _pools[entity.type].Release(entity);
    }
    #region PoolEvents

    private static void ActionOnDestroy(Entity obj)
    {
      Object.Destroy(obj.gameObject);
    }

    private static void ActionOnRelease(Entity obj)
    {
      obj.gameObject.SetActive(false);
      obj.onRelease?.Invoke();
    }

    private static void ActionOnGet(Entity obj)
    {
      obj.position = _tmpPos;
      obj.name = $"{obj.type}";
      obj.onGet?.Invoke();
      obj.gameObject.SetActive(true);
    }

    private static async Task<Entity> CreateFunc(string address)
    {
      var asset = await address.LoadAssetAsync();
      var obj = Object.Instantiate(asset, asset.GetComponent<Entity>() is UIEntity ? uiEntityParent : entityParent).GetComponent<Entity>();
      obj.type = address;
      return obj;
    }

  #endregion

  }
}
