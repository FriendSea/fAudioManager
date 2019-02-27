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

        public abstract float Volume { get; set; }

        public abstract void Start();
        public abstract void Stop();
        public abstract void Resume();
    }
}
