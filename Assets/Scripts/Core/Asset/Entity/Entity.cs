using UnityEngine;

namespace Core.Asset.Entity
{
  public abstract class Entity : MonoBehaviour
  {
    public delegate void EntityEventListener();

    public string type { get; set; }

    public EntityEventListener onGet;
    public EntityEventListener onRelease;
    
    public virtual Vector2 position {
      get => transform.position;
      set => transform.position = value;
    }
  }
}
