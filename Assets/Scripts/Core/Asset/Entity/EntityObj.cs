using System.Linq;
using UnityEngine;

namespace Core.Asset.Entity
{
  public class EntityObj : MonoBehaviour
  {
    public delegate void EntityEventListener();

    public string type { get; set; }

    public EntityEventListener onGet;
    public EntityEventListener onRelease;
    
    public virtual Vector2 position {
      get => transform.position;
      set => transform.position = value;
    }

    public void Release() => EntityMgr.Kill(this);

    private void Awake()
    {
      var components = GetComponents(typeof(IEntityCallbackReceiver));
      if (!components.Any()) return;

      foreach (var component in components)
      {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var callbackReceiver = component as IEntityCallbackReceiver;
        onGet += () => callbackReceiver!.OnSummon();
        onRelease += callbackReceiver!.OnKilled;
      }
    }
  }
}
