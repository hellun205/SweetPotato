using System.Collections.Generic;
using System.Linq;
using Core.Asset.Entity;
using Core.Data;
using UnityEngine;

namespace Test
{
  public class TestData
  {
    public string name { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
  }
  
  public class Test : MonoBehaviour
  {
    // public AssetReference ar;
    //
    // public Object ov;
    // private void Update()
    // {
    //   if (Input.GetKeyDown(KeyCode.Alpha1))
    //   {
    //     "TEST".LoadAsset(x => {
    //       Instantiate(x);
    //       ov = x;
    //     });
    //   }
    //   else if (Input.GetKeyDown(KeyCode.Alpha2))
    //   {
    //     ar.Load(x => {
    //      Instantiate(x);
    //       ov = x;
    //     });
    //   } else if (Input.GetKeyDown(KeyCode.Alpha3))
    //   {
    //     Addressables.Release(ov.GameObject());
    //   }
    // }
    private void Start()
    {
      // var x = DataMgr.FromCsv<TestData>("./Data/test.csv");
      // foreach (var testData in x.ToList())
      // {
      //   Debug.Log($"{testData.name} : {testData.age}살, {testData.gender}성");
      // }
    }
    public string assetAddr;
    public List<Entity> ls = new List<Entity>();
    private async void Update()
    {
      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
        Debug.Log("X");
        var e = await EntityMgr.Summon(assetAddr, Vector2.zero);
        ls.Add(e);
        Debug.Log(e.name);
      }
      else if (Input.GetKeyDown(KeyCode.Alpha2))
      {
        EntityMgr.Kill(ls[^1]);
        ls.RemoveAt(ls.Count - 1);
      } else if (Input.GetKeyDown(KeyCode.Alpha3))
      {
        
      }
    }
  }
}
