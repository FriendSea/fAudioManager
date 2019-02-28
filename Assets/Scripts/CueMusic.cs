using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    public class CueMusic : MonoBehaviour
    {
        [SerializeField]
        MusicBase music;
        [SerializeField]
        bool CueOnEnable = true;

        private void OnEnable()
        {
            if (CueOnEnable)
                Cue();
        }

        public void Cue()
        {
            MusicManager.Instance.CueMusic(music);
        }

        public void End()
        {
            MusicManager.Instance.EndMusic(music);
        }
    }
}
