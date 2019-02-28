using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    public class SEManager : SingletonBehaviour<SEManager>
    {
        [SerializeField]
        AudioSource SourceTemplate;
        [SerializeField]
        AudioSource UnSpacialSource;
        const uint maxSounds = 8;
        AudioSource[] sources = new AudioSource[maxSounds];
        int currentSource = 0;

        new void Awake()
        {
            base.Awake();
            for (int i = 0; i < maxSounds; i++)
            {
                sources[i] = Instantiate(SourceTemplate, transform);
            }
        }

        AudioSource GetSESource()
        {
            currentSource++;
            if (currentSource >= maxSounds) currentSource = 0;
            return sources[currentSource];
        }

        public void PlaySE(AudioClip clip, float volume)
        {
            UnSpacialSource.PlayOneShot(clip, volume);
        }

        public void PlaySE(AudioClip clip, float volume, Vector3 position)
        {
            var source = GetSESource();
            source.transform.position = position;
            source.PlayOneShot(clip, volume);
        }
    }
}
