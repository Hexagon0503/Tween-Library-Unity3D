using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.SplashScreen
{
	public class SplashScreenFade : SplashScreenBase
	{
		[Header("Duration")]
		[Range(0.1f, 1f)]public float fadeDuration = 1f;
		public float Duration = 2f;
		[Header("Audio")]
		public AudioClip effectAudio;
		//
		private CanvasGroup _group = null;
		private SplashManager handler = null;
		private Sequence _sequence = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Manager"></param>
		public override void Initialize(SplashManager Manager)
		{
			handler = Manager;
			gameObject.SetActiveOptimized(false);
			_group = GetComponent<CanvasGroup>();
		}

		/// <summary>
		/// 
		/// </summary>
		public override void ResetUI()
		{
			gameObject.SetActiveOptimized(false);
			_sequence.Kill();
			_sequence = null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="callBack"></param>
		public override void Play(System.Action callBack)
		{
			_sequence = DOTween.Sequence()
				.AppendCallback(() => 
					{ 
						gameObject.SetActiveOptimized(true); 
						_group.alpha = 0f;
						handler.PlayAudio(effectAudio);
					})
				.Append(_group.DOFade(1f, fadeDuration))
				.AppendInterval(Duration)
				.Append(_group.DOFade(0f, fadeDuration))
				.AppendCallback(() => { 					
					gameObject.SetActiveOptimized(false); 
					if(callBack != null){ callBack(); }
				});
		}


		/// <summary>
		/// 
		/// </summary>
		private void OnDisable()
		{
			ResetUI();
		}
	}
}