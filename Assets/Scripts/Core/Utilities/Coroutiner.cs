using System;
using System.Collections;
using Core.Management;
using UnityEngine;

namespace Core.Utilities
{
  public class Coroutiner
  {
    public Coroutine current { get; private set; }

    private Func<IEnumerator> routine;

    public Coroutiner(Func<IEnumerator> routine)
    {
      this.routine = routine;
    }

    public void Start()
    {
      Stop();
      current = Manager.StartCoroutine(routine.Invoke());
    }

    public void Stop()
    {
      if (current is not null)
        Manager.StopCoroutine(current);
    }
  }

  public static class CoroutineUtility
  {
    public static Coroutiner ToCoroutiner(this Func<IEnumerator> routine) => new Coroutiner(routine);
    public static Coroutiner Start(this Func<IEnumerator> routine)
    {
      var crt = new Coroutiner(routine);
      crt.Start();
      return crt;
    }
  }
}
