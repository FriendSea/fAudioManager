using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea
{
    [CreateAssetMenu]
	public class SoundEffect : ScriptableObject, ISoundEffect
    {
        [SerializeField]
        AudioClip clip;
        [SerializeField, Range(0, 2)]
        float volume = 1f;
		[SerializeField]
		bool spacial = true;
		[SerializeField]
		bool exclusive = false;
		[SerializeField]
		bool synced = false;

		public bool Spacial { get { return spacial; }}

		public bool Exclusive { get { return exclusive; }}

		public AudioClip Clip{ get { return clip; }}

		public float Volume { get { return volume; }}

		public bool Synced{ get { return synced; }}

		public void Play()
        {
			SEManager.Instance.PlaySE(this, null);
        }

		public void Play(Transform transform)
		{
			SEManager.Instance.PlaySE(this, transform);
		}
	}
}
