using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;
using UnityEngine;

namespace UKnack.Concrete.Audio
{
    [AddComponentMenu("UKnack/Audio/AudioSourcesVolumeUpdater")]
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudioSourcesVolumeUpdater : MonoBehaviour, ISubscriberToEvent<float>
    {
        public string Description => _description;
        public void OnEventNotification(float volume) =>
            UpdateVolume(volume);

        [SerializeField]
        [ValidReference] 
        private SOValue<float> _volumeProvider;

        private AudioSource[] _audiosources;
        private float[] _defaultVolumes;

        private string _description;

        private void UpdateVolume(float volume)
        {
            for (int i = 0; i < _audiosources.Length; i++)
            {
                //Debug.Log($"{volume} {_defaultVolumes[i]}");
                _audiosources[i].volume = volume * _defaultVolumes[i];
            }
        }

        private void OnEnable()
        {
            _description = $"{nameof(AudioSourcesVolumeUpdater)} of {gameObject.name}";

            InitFieldsOrThrow();

            _defaultVolumes = new float[_audiosources.Length];
            for (int i = 0; i < _audiosources.Length; i++)
                _defaultVolumes[i] = _audiosources[i].volume;

            UpdateVolume(_volumeProvider.GetValue());
            _volumeProvider.Subscribe(UpdateVolume);
        }

        private void OnDisable()
        {
            _volumeProvider.UnsubscribeNullSafe(UpdateVolume);
        }

        private void InitFieldsOrThrow()
        {
            if (_volumeProvider == null)
                throw new ArgumentNullException(nameof(_volumeProvider));
            _audiosources = GetComponents<AudioSource>();
            if (_audiosources == null)
                throw new ArgumentNullException(nameof(_audiosources));
            if (_audiosources.Length == 0)
                throw new Exception("There should be at least one Audiosource on Gameobject for this script to work");
        }


    }
}
