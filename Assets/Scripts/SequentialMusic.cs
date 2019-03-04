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
        [SerializeField]
        int nextTrack = -1;
        Block currentBlock;
        Block subBlock;

        [SerializeField, Range(0, 2)]
        float volume = 1f;
        public override float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                foreach (var b in Blocks)
                    b.track.Volume = value;
            }
        }

        public override float CurrentTime { get { return currentBlock.track.CurrentTime; } }

        public override void Resume()
        {
            if (currentBlock == null) return;
            currentBlock.track.Resume();
            if (subBlock != null)
                subBlock.track.Resume();
            MusicManager.Instance.OnBeat += OnBeat;
        }

        public override void Start()
        {
            currentBlock = Blocks[0];
            subBlock = null;
            currentBlock.track.Start();
            MusicManager.Instance.OnBeat += OnBeat;
        }

        public override void Stop()
        {
            if (currentBlock != null)
                currentBlock.track.Stop();
            if (subBlock != null)
                subBlock.track.Stop();
            MusicManager.Instance.OnBeat -= OnBeat;
        }

        void OnBeat(uint beat)
        {
            var next = nextTrack < 0 ?
                currentBlock : Blocks[nextTrack];
            //次のセクションの開始時刻
            if (beat == currentBlock.SwitchBeat - next.StartBeat)
            {
                subBlock = next;
                subBlock.track.Start();
            }
            //セクションの切り替わり時刻
            if (beat == currentBlock.SwitchBeat)
            {
                var tmp = currentBlock;
                currentBlock = subBlock;
                subBlock = tmp;
                nextTrack++;
                if (nextTrack >= Blocks.Length)
                    nextTrack = 0;
            }
            //前のセクションの終了時刻
            if (subBlock != null)
                if (beat == subBlock.FinalBeat - subBlock.SwitchBeat)
                {
                    subBlock.track.Stop();
                }
        }

        [System.Serializable]
        class Block
        {
            public MusicBase track;
            public uint StartBeat;
            public uint SwitchBeat;
            public uint FinalBeat;
        }
    }
}
