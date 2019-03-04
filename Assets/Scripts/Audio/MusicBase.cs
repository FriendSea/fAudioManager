using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    public abstract class MusicBase : ScriptableObject
    {
        [SerializeField]
        uint priority;
        public uint Priority { get { return priority; } }

        [SerializeField]
        uint BPM;
        public uint Tempo{get{return BPM; } }

        public abstract float Volume { get; set; }

        public abstract float CurrentTime { get; }

        public void CuePlay()
        {
            MusicManager.Instance.CueMusic(this);
        }

        public void CueEnd()
        {
            MusicManager.Instance.CueEnd(this);
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void Resume();

    }
}
