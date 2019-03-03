using UnityEngine;

namespace FriendSea{
	public class SEPlayer : MonoBehaviour, ISoundEffect
	{
		[SerializeField]
		SoundEffect Data;
		[SerializeField]
		bool PlayOnEnable = true;
		[SerializeField]
		bool ExclusiveParPlayer;

		public AudioClip Clip{ get { return Data.Clip; }}

		public float Volume { get { return Data.Volume; }}

		public bool Spacial { get { return Data.Spacial; }}

		public bool Exclusive { get { return Data.Exclusive || ExclusiveParPlayer; }}

		public bool Synced{ get {return Data.Synced; }}

		public void Play(){
			if (ExclusiveParPlayer)
				SEManager.Instance.PlaySE(this, transform);
			else
				SEManager.Instance.PlaySE(Data, transform);
		}

		void OnEnable()
		{
			if (PlayOnEnable)
				Play();
		}
	}
}