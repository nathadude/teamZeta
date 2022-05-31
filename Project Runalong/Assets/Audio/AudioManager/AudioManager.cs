using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	//public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	private AudioSource activeSong;
	private AudioSource stoppableTrack;

	void Awake()
	{
		if (instance != null)
		{
			Debug.Log("Destroying audiomanager, already present");
			Destroy(gameObject);
			return;
		} else
		{
			Debug.Log("Make an audiomanager");
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}

	/// <summary>
	/// Plays a music track. Overrides the currently playing music track, if any.
	/// </summary>
	public void PlayMusic(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		if (activeSong != null)
		{
			activeSong.Stop();
		}
		activeSong = s.source;
		s.source.Play();
	}

	/// <summary>
	/// Stops the active music track
	/// </summary>
	public void StopMusic()
    {
		if (activeSong != null)
		{
			activeSong.Stop();
			activeSong = null;
		}
	}

	/// <summary>
	/// Plays a song only if something is not already playing
	/// </summary>
	public void PlayMusicIfNotPlaying(string sound)
	{
		if (activeSong != null) return;
		PlayMusic(sound);
	}

	/// <summary>
	/// Plays a sound. This sound cannot be stopped - ideal for short SFX
	/// </summary>
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		s.source.Play();
	}

	/// <summary>
	/// Plays a sound, pitched according to pitchMod (1 = normal pitch)
	/// </summary>
	public void PlayPitch(string sound, float pitchMod)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch * pitchMod;

		s.source.Play();
	}

	// Play a track that can be stopped later with StopStoppableTrack.
	// Do not play more than one stoppable track at a time.
	public void PlayStoppableTrack(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		//Debug.Log("Play track");
		stoppableTrack = s.source;
		s.source.Play();
	}

	public void StopStoppableTrack()
	{
		if (stoppableTrack == null) return;
		stoppableTrack.Stop();
		stoppableTrack = null;
	}

	public void FadeOutStoppableTrack()
    {
		if (stoppableTrack == null) return;
		StartCoroutine(FadeOut(stoppableTrack, 0.25f));
	}

	// Fades out for fadeTime, or until a new stoppable track is started
	IEnumerator FadeOut(AudioSource source, float fadeTime)
    {
		//Debug.Log("Fading out");
		stoppableTrack = null;
		float startVolume = source.volume;
		while (source.volume > 0 && stoppableTrack == null)
        {
			source.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
        }
		//Debug.Log("Done fading");
		if (stoppableTrack == null || stoppableTrack.name == source.name) source.Stop();
		source.volume = startVolume;
    }

}
