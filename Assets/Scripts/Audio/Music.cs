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
        MusicTrack track = new MusicTrack();

        public override float Volume
        {
            get { return track.Volume; }
            set { track.Volume = value; }
        }

        public override float CurrentTime { get { return track.CurrentTime; } }

        public override void Start()
        {
            track.Start(Clip);
        }

        public override void Stop()
        {
            track.Stop();
        }

        public override void Resume()
        {
            track.Resume(Clip);
        }
    }
}
