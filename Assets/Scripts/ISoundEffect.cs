using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{

    public interface ISoundEffect
    {
        AudioClip Clip { get; }
        float Volume { get; }
        bool Spacial { get; }
        bool Exclusive { get; }
    }
}
