using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

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

        public IEnumerable<MusicBase> CueList { get {
                return musics;
            } }

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
        public void CueEnd(MusicBase music)
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

        public UnityAction<uint> OnBeat { get; set; }
        UnityAction<uint> OnBeatOnce { get; set; }

        public uint GetCurrentBeat()
        {
            if (current == null) return 0;
            return (uint)(current.CurrentTime * current.Tempo / 60f);
        }

        public void SyncedCall(UnityAction<uint> action)
        {
            OnBeatOnce += action;
        }

        uint beforeBeat = 0;
        void FixedUpdate()
        {
            if (current == null)
            {
                if (OnBeatOnce != null)
                    OnBeatOnce(0);
                OnBeatOnce = null;
                return;
            }
            var beat = GetCurrentBeat();
            if (beat != beforeBeat)
            {
                if (OnBeat != null)
                    OnBeat(beat);
                if (OnBeatOnce != null)
                    OnBeatOnce(beat);
                OnBeatOnce = null;
            }
            beforeBeat = beat;
        }
    }
}
