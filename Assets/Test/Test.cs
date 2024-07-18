using System.Linq;
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
      var x = DataMgr.FromCsv<TestData>("./Data/test.csv");
      foreach (var testData in x.ToList())
      {
        Debug.Log($"{testData.name} : {testData.age}살, {testData.gender}성");
      }
    }
  }
}
