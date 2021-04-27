﻿using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Music
    {
        Music,
        Titlescreen,

        Count
    }

    public enum Sfx
    {
        heyListen,
        itemCatch,
        martha,

        Count
    };



    public AudioClip[] MusicAudioClips;
    public AudioClip[] SfxAudioClips;

    public AudioSource MusicAudioSource { get; private set; }


    public void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/Resources.html

        MusicAudioClips = Resources.LoadAll<AudioClip>("audio/music");
        Debug.Assert((int)Music.Count == MusicAudioClips.Length, "SoundManager : Music enum length (" + (int)Music.Count + ") does not match Resources folder (" + MusicAudioClips.Length + ")");

        SfxAudioClips = Resources.LoadAll<AudioClip>("audio/sfx");
        Debug.Assert((int)Sfx.Count == SfxAudioClips.Length, "SoundManager : Sfx enum length " + (int)Sfx.Count + ") does not match Resources folder (" + SfxAudioClips.Length + ")");



        // https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
        MusicAudioSource = gameObject.AddComponent<AudioSource>();
        MusicAudioSource.loop = true;
        MusicAudioSource.volume = 0.08f;
    }

    public void Play(Music music)
    {
        MusicAudioSource.clip = MusicAudioClips[(int)music];
        MusicAudioSource.Play();
    }

    public void Stop(Music music)
    {
        MusicAudioSource.clip = MusicAudioClips[(int)music];
        MusicAudioSource.Stop();
    }

    public void Play(Sfx sfx)
    {
        AudioSource.PlayClipAtPoint(SfxAudioClips[(int)sfx], transform.position);
    }


}
