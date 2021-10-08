using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioCue", menuName = "Audio/Cue"), System.Serializable]
public class AudioCue : ScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup audioMixer;
    public bool loop = false;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0f, 2f)] public float pitch = 1f;
    [Range(-1f, 1f)] public float stereoPan = 0;
    [Range(0f, 1f)] public float spatialBlend = 0f;
    [Range(0f, 1.1f)] public float reverbZoneMix = 1f;
}
