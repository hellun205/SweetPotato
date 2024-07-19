using System;
using System.Collections.Generic;
using Core.Management;
using Core.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Asset.Audio
{
  public static class AudioMgr
  {
    public enum PlayType
    {
      Default,
      OneShot
    }

    public static GameObject audioPlayObj { get; private set; }

    private static Dictionary<AudioType, AudioSource> _audioSources = new Dictionary<AudioType, AudioSource>();

    public static AudioMixer audioMixer { get; private set; }

    private static readonly Dictionary<AudioType, PlayType> AudioPlayTypes = new Dictionary<AudioType, PlayType>
    {
      { AudioType.Master, PlayType.OneShot },
      { AudioType.Bgm, PlayType.Default },
      { AudioType.Sfx, PlayType.OneShot },
      { AudioType.SfxNormal, PlayType.OneShot },
      { AudioType.SfxUi, PlayType.OneShot }
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
      Manager.CallInit(() => {
        audioPlayObj = new GameObject("[Audio Player]", typeof(DontDestroyOnLoadObject));
        audioMixer = Resources.Load<AudioMixer>("AudioMixer");

        var types = Utility.GetValues<AudioType>();
        foreach (var audioType in types)
        {
          var audSrc = audioPlayObj.AddComponent<AudioSource>();
          audSrc.playOnAwake = false;
          audSrc.outputAudioMixerGroup = audioMixer.FindMatchingGroups(audioType.ToString())[0];
          _audioSources.Add(audioType, audSrc);
        }
      });
    }

    public static void SetVolume(AudioType type, float vol)
    {
      audioMixer.SetFloat($"{type.ToString()}Vol", Mathf.Log10(vol) * 20f);
    }

    public static void Play(AudioType type, AudioClip clip, bool loop = false)
    {
      var audSrc = _audioSources[type];
      switch (AudioPlayTypes[type])
      {
        case PlayType.Default:
          audSrc.loop = loop;
          Stop(type);
          audSrc.clip = clip;
          audSrc.Play();
          break;
        
        case PlayType.OneShot:
          audSrc.PlayOneShot(clip);
          break;
      }
    }

    public static async void Play(AudioType type, string assetAddress, bool loop = false)
    {
      var clip = await assetAddress.LoadAssetAsync<AudioClip>();
      Play(type, clip, loop);
    }

    public static void Stop(AudioType type)
    {
      if (AudioPlayTypes[type] != PlayType.Default)
        throw new Exception("Cannot stop because AudioType is not Default.");

      _audioSources[type].Stop();
    }
  }
}
