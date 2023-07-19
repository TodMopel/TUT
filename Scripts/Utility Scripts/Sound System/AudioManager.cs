using System;
using UnityEngine;
using UnityEngine.Audio;

namespace TodMopel
{
	public class AudioManager : MonoBehaviour
	{
		public AudioMixerGroup musicMixer;
		public SoundClip[] music;
		public AudioMixerGroup soundsMixer;
		public SoundClip[] sounds;
		public static AudioManager instance;

		private void Awake()
		{
			if (instance == null)
				instance = this;
			else {
				Destroy(gameObject);
				return;
			}
			DontDestroyOnLoad(gameObject);

			CreateSoundClipComponents(music, musicMixer);
			CreateSoundClipComponents(sounds, soundsMixer);
		}

		private void CreateSoundClipComponents(SoundClip[] audio, AudioMixerGroup mixer)
		{
			foreach (SoundClip s in audio) {
				s.source = gameObject.AddComponent<AudioSource>();
				s.source.outputAudioMixerGroup = mixer;
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
				s.source.playOnAwake = false;
			}
		}

		#region SOUNDS
		public void PlaySound(string name)
		{
			SoundClip s = Array.Find(sounds, sounds => sounds.name == name);
			if (s == null) {
				Debug.LogWarning("Play SoundClip : " + name + " not found!");
				return;
			}
			s.source.Play();
		}

		public void StopSound(string name)
		{
			SoundClip s = Array.Find(sounds, sounds => sounds.name == name);
			if (s == null) {
				Debug.LogWarning("Stop SoundClip : " + name + " not found!");
				return;
			}
			s.source.Stop();
		}

		public bool SoundIsPlaying(string name)
		{
			SoundClip s = Array.Find(sounds, sounds => sounds.name == name);
			if (s == null) {
				Debug.LogWarning("Set SoundClip Volume : " + name + " not found!");
				return false;
			}
			return s.source.isPlaying;
		}
		#endregion

		#region MUSIC
		public string currentMusic = "empty";
		public void PlayMusic(string name)
		{
			SoundClip s = Array.Find(music, music => music.name == name);
			if (s == null) {
				Debug.LogWarning("Play Music : " + name + " not found!");
				return;
			}
			currentMusic = name;

			s.source.Play();
		}

		public void StopMusic(string name)
		{
			SoundClip s = Array.Find(music, music => music.name == name);
			if (s == null) {
				Debug.LogWarning("Stop Music : " + name + " not found!");
				return;
			}
			s.source.Stop();
		}

		public void StopCurrentMusicIfDiferentTo(string name)
		{
			Debug.Log("currentMusic : "+ currentMusic+ ", name : "+ name);
			if (currentMusic != "empty" && currentMusic != name)
				StopMusic(currentMusic);
		}

		public bool MusicIsPlaying(string name)
		{
			SoundClip s = Array.Find(music, music => music.name == name);
			if (s == null) {
				Debug.LogWarning("Set Music Volume : " + name + " not found!");
				return false;
			}
			return s.source.isPlaying;
		}
		#endregion
	}

}