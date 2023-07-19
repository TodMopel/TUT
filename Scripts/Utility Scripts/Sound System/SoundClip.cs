using UnityEngine;
using UnityEngine.Audio;

namespace TodMopel
{
    [System.Serializable]
    public class SoundClip
    {
        public string name = "audio name";
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1;
        [Range(0.1f, 3f)]
        public float pitch = 1;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}