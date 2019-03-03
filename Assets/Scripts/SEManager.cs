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
		SESource[] sources = new SESource[maxSounds];

        new void Awake()
        {
            base.Awake();
            for (int i = 0; i < maxSounds; i++)
            {
				sources[i] = new SESource(Instantiate(SourceTemplate, transform));
            }
        }

		void Update()
		{
			foreach (var s in sources)
				s.UpdatePosition();
		}

		int currentSource = 0;
		SESource GetSESource()
        {
            currentSource++;
            if (currentSource >= maxSounds) currentSource = 0;
			return sources[currentSource];
        }

		public void PlaySE(ISoundEffect se, Transform transform){
			if (se.Synced)
				MusicManager.Instance.SyncedCall((beat) => PlayImmediate(se, transform));
			else
				PlayImmediate(se, transform);
		}

		void PlayImmediate(ISoundEffect se, Transform transform)
		{
			if (se.Exclusive)
				foreach (var s in sources)
					if (s.soundEffect == se)
						s.Stop();
			GetSESource().Play(se, transform);
		}

		class SESource{
			AudioSource audioSource;
			public ISoundEffect soundEffect { get; private set; }
			Transform transform;

			public SESource(AudioSource source){
				audioSource = source;
			}

			public void Play(ISoundEffect sound, Transform position){
				soundEffect = sound;
				transform = position;
				audioSource.spatialBlend = (soundEffect.Spacial && transform != null) ? 1f : 0f;
				UpdatePosition();
				audioSource.PlayOneShot(sound.Clip, sound.Volume);
			}

			public void Stop(){
				soundEffect = null;
				audioSource.Stop();
			}

			public void UpdatePosition(){
				if (soundEffect == null) return;
				if (transform == null) return;
				if (!soundEffect.Spacial) return;
				audioSource.transform.position = transform.position;
			}
		}
    }
}
