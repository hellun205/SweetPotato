using UnityEngine;

namespace Core.Management
{
  public sealed class DontDestroyOnLoadObject : MonoBehaviour
  {
    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
      Destroy(this);
    }
  }
}
