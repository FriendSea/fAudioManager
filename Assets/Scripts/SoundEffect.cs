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
        [SerializeField]
        bool Synced = false;

        public void Play()
        {
            if (Synced)
                MusicManager.Instance.SyncedCall((beat) => SEManager.Instance.PlaySE(Clip, Volume));
            else
                SEManager.Instance.PlaySE(Clip, Volume);
        }

        public void PlaySpatial(Vector3 position)
        {
            if (Synced)
                MusicManager.Instance.SyncedCall((beat) => SEManager.Instance.PlaySE(Clip, Volume, position));
            else
                SEManager.Instance.PlaySE(Clip, Volume, position);
        }

        public void PlaySpatial(Transform transform)
        {
            if (Synced)
                MusicManager.Instance.SyncedCall((beat) => SEManager.Instance.PlaySE(Clip, Volume, transform.position));
            else
                SEManager.Instance.PlaySE(Clip, Volume, transform.position);
        }
    }
}
