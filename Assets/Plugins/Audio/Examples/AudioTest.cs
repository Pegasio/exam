using Plugins.Audio.Core;
using Plugins.Audio.Utils;
using UnityEngine;

namespace Plugins.Audio.Examples
{
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] private SourceAudio _musicSource;
        [SerializeField] private SourceAudio _soundSource;
        [SerializeField] private AudioDataProperty _musicClip;
        [SerializeField] private AudioDataProperty _soundClip;

        private void Start()
        {
            _musicSource.Play(_musicClip.Key);
            _musicSource.Mute = true;
            _musicSource.Mute = false;

        }

        

        public void PlaySound()
        {
            _soundSource.Play(_soundClip.Key);
        }
    }
}
