using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    [CreateAssetMenu]
    public class SequentialMusic : MusicBase
    {
        [SerializeField]
        Block[] Blocks;

        public int CurrentBlock { get { return System.Array.IndexOf(Blocks, currentBlock); } }
        public int NextBlock { get; set; }

        Block currentBlock;
        MusicTrack currentTrack = new MusicTrack();
        Block subBlock;
        MusicTrack subTrack = new MusicTrack();

        [SerializeField, Range(0, 2)]
        float volume = 1f;
        public override float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                currentTrack.Volume = value;
                subTrack.Volume = value;
            }
        }

        public override float CurrentTime { get { return currentTrack.CurrentTime; } }

        public override void Resume()
        {
            if (currentBlock == null) {
                Start();
                return;
            }
            currentTrack.Resume(currentBlock.Clip);
            if (subBlock != null)
                subTrack.Resume(subBlock.Clip);
            MusicManager.Instance.OnBeat += OnBeat;
        }

        public override void Start()
        {
            currentBlock = Blocks[0];
            subBlock = null;
            currentTrack.Start(currentBlock.Clip);
            subTrack.Stop();
            MusicManager.Instance.OnBeat += OnBeat;
        }

        public override void Stop()
        {
            currentTrack.Stop();
            subTrack.Stop();
            MusicManager.Instance.OnBeat -= OnBeat;
        }

        void OnBeat(uint beat)
        {
            var next = (NextBlock < 0 || NextBlock >= Blocks.Length) ?
                currentBlock : Blocks[NextBlock];
            //次のセクションの開始時刻
            if (beat == currentBlock.SwitchBeat - next.StartBeat)
            {
                subBlock = next;
                subTrack.Start(subBlock.Clip);
            }
            //セクションの切り替わり時刻
            if (beat == currentBlock.SwitchBeat)
            {
                var blk = currentBlock;
                currentBlock = subBlock;
                subBlock = blk;
                var trk = currentTrack;
                currentTrack = subTrack;
                subTrack = trk;
            }
            //前のセクションの終了時刻
            if (subBlock != null)
                if (beat == subBlock.FinalBeat - subBlock.SwitchBeat)
                {
                    subTrack.Stop();
                    subBlock = null;
                }
        }

        [System.Serializable]
        class Block
        {
            public AudioClip Clip;
            public uint StartBeat;
            public uint SwitchBeat;
            public uint FinalBeat;
        }
    }
}
