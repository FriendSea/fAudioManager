using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    [CreateAssetMenu]
    public class SoundEffect : ScriptableObject
    {
        [SerializeField]
        AudioClip Clip;
        [SerializeField, Range(0, 2)]
        float Volume = 1f;

        public void Play()
        {
            SEManager.Instance.PlaySE(Clip, Volume);
        }

        public void PlaySpatial(Vector3 psition)
        {
            SEManager.Instance.PlaySE(Clip, Volume, psition);
        }

        public void PlaySpatial(Transform transform)
        {
            SEManager.Instance.PlaySE(Clip, Volume, transform.position);
        }
    }
}
