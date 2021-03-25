using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum ShooterMusic
    {
        Music,

        Count
    }

    public enum ShooterSfx
    {
        Explosion,
        Hit,
        Hurt,
        Pickup,
        Pistol,
        Shotgun,
        Spawn,

        Count
    };

    public enum PlatformerMusic
    {
        GameOver,
        Music,

        Count
    }

    public enum PlatformerSfx
    {
        Block,
        Coin,
        Dead,
        EnemyFireball,
        Fireball,
        Hit,
        Item,
        Jump,
        Kick,
        LevelEnter,
        Powerup,
        Stomp,
        Thwomp,

        Count
    };

    public AudioClip[] ShooterMusicAudioClips;
    public AudioClip[] ShooterSfxAudioClips;

    public AudioClip[] PlatformerMusicAudioClips;
    public AudioClip[] PlatformerSfxAudioClips;

    public AudioSource MusicAudioSource { get; private set; }


    public void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/Resources.html
        ShooterMusicAudioClips = Resources.LoadAll<AudioClip>("shooter/audio/music");
        Debug.Assert((int)ShooterMusic.Count == ShooterMusicAudioClips.Length, "SoundManager : Music enum length (" + (int)ShooterMusic.Count + ") does not match Resources folder (" + ShooterMusicAudioClips.Length + ")");

        ShooterSfxAudioClips = Resources.LoadAll<AudioClip>("shooter/audio/sfx");
        Debug.Assert((int)ShooterSfx.Count == ShooterSfxAudioClips.Length, "SoundManager : Sfx enum length " + (int)ShooterSfx.Count + ") does not match Resources folder (" + ShooterSfxAudioClips.Length + ")");

        PlatformerMusicAudioClips = Resources.LoadAll<AudioClip>("platformer/audio/music");
        Debug.Assert((int)PlatformerMusic.Count == PlatformerMusicAudioClips.Length, "SoundManager : Music enum length (" + (int)PlatformerMusic.Count + ") does not match Resources folder (" + PlatformerMusicAudioClips.Length + ")");

        PlatformerSfxAudioClips = Resources.LoadAll<AudioClip>("Platformer/audio/sfx");
        Debug.Assert((int)PlatformerSfx.Count == PlatformerSfxAudioClips.Length, "SoundManager : Sfx enum length " + (int)PlatformerSfx.Count + ") does not match Resources folder (" + PlatformerSfxAudioClips.Length + ")");



        // https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
        MusicAudioSource = gameObject.AddComponent<AudioSource>();
        MusicAudioSource.loop = true;
        MusicAudioSource.volume = 0.08f;
    }

    public void Play(PlatformerMusic music)
    {
        MusicAudioSource.clip = PlatformerMusicAudioClips[(int)music];
        MusicAudioSource.Play();
    }

    public void Stop(PlatformerMusic music)
    {
        MusicAudioSource.clip = PlatformerMusicAudioClips[(int)music];
        MusicAudioSource.Stop();
    }

    public void Play(PlatformerSfx sfx)
    {
        AudioSource.PlayClipAtPoint(PlatformerSfxAudioClips[(int)sfx], transform.position);
    }

    public void Play(ShooterMusic music)
    {
        MusicAudioSource.clip = ShooterMusicAudioClips[(int)music];
        MusicAudioSource.Play();
    }

    public void Play(ShooterSfx sfx)
    {
        AudioSource.PlayClipAtPoint(ShooterSfxAudioClips[(int)sfx], transform.position);
    }
}
