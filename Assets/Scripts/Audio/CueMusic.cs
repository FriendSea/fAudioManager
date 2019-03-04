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
        bool EndOnDisable;

        void OnEnable()
        {
        	Cue();
        }

		void OnDisable()
		{
			if (EndOnDisable)
				End();
		}

		public void Cue()
        {
            MusicManager.Instance.CueMusic(music);
        }

        public void End()
        {
            MusicManager.Instance.CueEnd(music);
        }
    }
}
