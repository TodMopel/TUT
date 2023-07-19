using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TodMopel {
	public class AudioMixerController : MonoBehaviour
	{
		[SerializeField] private string valueName = "MasterVolume";
		[SerializeField] private AudioMixer audioMixer;
		[SerializeField] private FloatVariable audioVariable;
		private void Start()
		{
			Setvolume(audioVariable.value);
		}
		public void Setvolume(float value)
		{
			audioMixer.SetFloat(valueName, Mathf.Log10(audioVariable.value) * 20);
		}
	}
}
