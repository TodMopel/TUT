using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TodMopel
{
	public class MusicManagement : MonoBehaviour
	{
		public string mainMusicName = "empty";
		AudioManager audioManager;
		private IEnumerator Start()
		{
			yield return new WaitForEndOfFrame();
			audioManager = FindObjectOfType<AudioManager>();
			audioManager.StopCurrentMusicIfDiferentTo(mainMusicName);
			if (!audioManager.MusicIsPlaying(mainMusicName))
				audioManager.PlayMusic(mainMusicName);
		}

		private float lowFrequency = 1300, normalFrequency = 14000;
		public void ChangeLowPassFilterFrequency(bool paused)
		{
			if (paused)
				audioManager.musicMixer.audioMixer.SetFloat("MusicLowPassFrequency", lowFrequency);
			else
				audioManager.musicMixer.audioMixer.SetFloat("MusicLowPassFrequency", normalFrequency);
		}
		private void OnDestroy()
		{
			audioManager.musicMixer.audioMixer.SetFloat("MusicLowPassFrequency", normalFrequency);
		}
	}
}
