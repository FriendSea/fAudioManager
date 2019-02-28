using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FriendSea
{
    public class MusicManager : SingletonBehaviour<MusicManager>
    {
        #region
        [SerializeField]
        AudioSource SourceTemplate;
        const uint maxSounds = 4;
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

        public AudioSource GetMusicSource()
        {
            currentSource++;
            if (currentSource >= maxSounds) currentSource = 0;
            return sources[currentSource];
        }
        #endregion

        [SerializeField, Range(0, 5)]
        float Duration = 1f;

        List<MusicBase> musics = new List<MusicBase>();
        MusicBase current = null;
        const uint maxMusicCount = 8;
        public void CueMusic(MusicBase music)
        {
            if (musics.Contains(music)) musics.Remove(music);
            musics.Add(music);
            musics = musics.OrderBy(m => m.Priority).ToList();
            if (musics.Count > maxMusicCount)
                musics.RemoveAt(0);
            var newMusic = musics.Last();
            if (newMusic == current) return;
            var oldMusic = current;
            current = newMusic;

            if (oldMusic != null)
                Tween.Tween01((v) =>
                {
                    oldMusic.Volume = 1f - v;
                }, Duration, () =>
                {
                    oldMusic.Stop();
                    newMusic.Volume = 1f;
                    newMusic.Start();
                });
            else
            {
                newMusic.Volume = 1f;
                newMusic.Start();
            }
        }

        public void EndMusic(MusicBase music)
        {
            if (!musics.Contains(music)) return;
            musics.Remove(music);
            var newMusic = musics.LastOrDefault();
            if (newMusic == current) return;
            var oldMusic = current;
            current = newMusic;

            if (newMusic != null)
            {
                newMusic.Volume = 0f;
                newMusic.Resume();
            }
            Tween.Tween01((v) =>
            {
                oldMusic.Volume = 1f - v;
                if (newMusic != null)
                    newMusic.Volume = v;
            }, Duration, () =>
            {
                oldMusic.Stop();
                if (newMusic != null)
                    newMusic.Volume = 1f;
            });
        }
    }
}
