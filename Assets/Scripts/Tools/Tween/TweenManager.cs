using System.Collections.Generic;
using UnityEngine.Events;

namespace FriendSea{
	public class TweenManager : SingletonBehaviour<TweenManager> {
		List<Tweener> tweeners = new List<Tweener>();

		void Update()
		{
			for (int i = tweeners.Count - 1; i >= 0; i--)
				tweeners[i].Drive();
		}

		internal void AddTweener(Tweener tweener){
			if (tweeners.Contains(tweener)) return;
			tweeners.Add(tweener);
		}

		internal void RemoveTweener(Tweener tweener){
			if (!tweeners.Contains(tweener)) return;
			tweeners.Remove(tweener);
		}
	}

	public static class Tween { 
		public static Tweener Tween01(UnityAction<float> setter, float duration, UnityAction onComplete = null){
			return new Tweener(setter, duration, onComplete);
		}

		public static Tweener DelayedCall(float time, UnityAction callBack){
			return new Tweener(null, time, callBack);
		}
	}
}
