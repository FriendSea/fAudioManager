using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    [CreateAssetMenu]
    public class LayeredMusic : MusicBase
    {
        [SerializeField, Range(0, 1)]
        float controlValue;
        [SerializeField]
        Track[] Tracks;
        public float ControlValue
        {
            get { return controlValue; }
            set
            {
                controlValue = value;
                UpdateVolume();
            }
        }

        [SerializeField, Range(0, 2)]
        float volume = 1f;
        public override float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                UpdateVolume();
            }
        }

        public override float CurrentTime
        {
            get
            {
                return Tracks[0].source == null ?
                    currentTime :
                    Tracks[0].source.time;
            }
        }

        float currentTime;

        public override void Start()
        {
            currentTime = 0;
            Resume();
        }

        public override void Stop()
        {
            currentTime = Tracks[0].source.time;
            foreach (var t in Tracks)
            {
                if (t.source == null) continue;
                t.source.Stop();
                t.source = null;
            }
        }

        public override void Resume()
        {
            foreach (var t in Tracks)
            {
                var newsource = MusicManager.Instance.GetMusicSource();
                newsource.clip = t.Clip;
                newsource.time = currentTime;
                newsource.Play();
                t.source = newsource;
            }
            UpdateVolume();
        }

        void UpdateVolume()
        {
            foreach (var t in Tracks)
            {
                if (t.source == null) continue;
                t.source.volume = t.VolumeCurve.Evaluate(controlValue) * volume;
            }
        }

        [System.Serializable]
        class Track
        {
            public AudioClip Clip;
            public AnimationCurve VolumeCurve;
            [System.NonSerialized]
            public AudioSource source;
        }

        public void OnValidate()
        {
            UpdateVolume();
        }
    }
}
