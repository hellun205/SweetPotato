using Core.Utilities;
using UnityEngine;

namespace Core.Asset.Entity
{
  public abstract class UIEntityObj : EntityObj
  {
    private Vector2 _position;
    public override Vector2 position {
      get => _position;
      set {
        _position = value;
        transform.position = EntityMgr.uiEntityParent.GetComponent<Canvas>().WorldToCanvasPosition(value);
      }
    }
  }
}
