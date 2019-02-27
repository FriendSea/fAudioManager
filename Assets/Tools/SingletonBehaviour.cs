using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendSea{
	public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
	{

		static T _instance;
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null)
						Debug.Log("There is no instance of " + typeof(T));
				}
				return _instance;
			}
		}

		protected void Awake(){
			SetInstance();
		}

		void SetInstance(){
			if (_instance == null){
				_instance = (T)this;
				return;
			}
			if (Instance == this) return;

			Destroy(this);
		}

		void OnDestroy()
		{
			if (Instance == this)
				_instance = null;
		}
	}
}
