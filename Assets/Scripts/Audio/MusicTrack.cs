using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    public class MusicTrack
    {
        AudioSource source;

        float currentTime;
        public float CurrentTime
        {
            get
            {
                return source == null ?
                    currentTime :
                    source.time;
            }
        }

        float volume = 1f;
        public float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                if (source != null)
                    source.volume = value;
            }
        }

        public void Start(AudioClip Clip)
        {
            currentTime = 0;
            Resume(Clip);
        }

        public void Stop()
        {
            if (source == null)
                return;
            currentTime = source.time;
            source.Stop();
            source = null;
        }

        public void Resume(AudioClip Clip)
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
