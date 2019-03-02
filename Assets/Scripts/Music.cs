using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    [CreateAssetMenu]
    public class Music : MusicBase
    {
        [SerializeField]
        AudioClip Clip;

        float volume = 1f;
        public override float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                if (source != null)
                    source.volume = value;
            }
        }

        public override float CurrentTime { get {
                return source == null ?
                    currentTime :
                    source.time;
            } }

        AudioSource source;
        float currentTime;

        public override void Start()
        {
            currentTime = 0;
            Resume();
        }

        public override void Stop()
        {
            if (source == null)
                return;
            currentTime = source.time;
            source.Stop();
            source = null;
        }

        public override void Resume()
        {
            if (source == null)
                source = MusicManager.Instance.GetMusicSource();
            source.volume = Volume;
            source.clip = Clip;
            source.time = currentTime;
            source.Play();
        }
    }
}
