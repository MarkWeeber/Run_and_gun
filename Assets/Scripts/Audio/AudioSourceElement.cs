using UnityEngine;

namespace RunAndGun.Space
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceElement : MonoBehaviour
    {
        [SerializeField] private AudioType audioType = AudioType.SoundEffectType;
        private MainMenu mainMenuReference;
        private AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            mainMenuReference = FindObjectOfType<MainMenu>();
            if (audioType == AudioType.SoundEffectType)
            {
                mainMenuReference.AudioEffectsSource.Add(audioSource);
            }
            else if (audioType == AudioType.MusicType)
            {
                mainMenuReference.MusicSource.Add(audioSource);
            }
        }

        private void OnDestroy()
        {
            if (audioType == AudioType.SoundEffectType)
            {
                mainMenuReference.AudioEffectsSource.Remove(audioSource);
            }
            else if (audioType == AudioType.MusicType)
            {
                mainMenuReference.MusicSource.Remove(audioSource);
            }
        }

    }
}