using UnityEngine;
using UnityEngine.Events;

namespace FriendSea{
	public class Tweener {
		float Duration;
		UnityAction<float> Setter;
		UnityAction OnComplete;

		float time;

		public Tweener(UnityAction<float> setter, float duration, UnityAction onComplete){
			Setter=setter;
			Duration = duration;
			OnComplete = onComplete;
			TweenManager.Instance.AddTweener(this);
		}

		internal void Drive(){
			time += Time.unscaledDeltaTime;
			float t = time / Duration;
			if (t >= 1f) {
				Complete();
				return;
			}
			if (Setter != null)
				Setter(Ease(t));
		}

		public void Complete(){
			if (Setter != null)
				Setter(1f);
			if (OnComplete != null)
				OnComplete();
			TweenManager.Instance.RemoveTweener(this);
		}

		float Ease(float t){
			t = 1f - t;
			return 1f - t * t;
		}
	}
}
