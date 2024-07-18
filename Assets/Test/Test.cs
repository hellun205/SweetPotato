using Core.Asset;
using Core.Management;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class Test : MonoBehaviour
{
  public AssetReference ar;

  public Object ov;
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      "TEST".LoadAsset(x => {
        Instantiate(x);
        ov = x;
      });
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      ar.Load(x => {
       Instantiate(x);
        ov = x;
      });
    } else if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      Addressables.Release(ov.GameObject());
    }
  }
}
