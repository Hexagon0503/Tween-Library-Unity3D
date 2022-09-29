using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#endif
namespace Game.SplashScreen
{
	public class SplashScreenPPV : SplashScreenBase
	{
		[Header("Duration")]
		[SerializeField] private VolumeFrame[] Frames = null;
#if UNITY_POST_PROCESSING_STACK_V2
		[SerializeField] private PostProcessVolume ppV = null;
#endif
		//
		private SplashManager handler = null;
		private Sequence _sequence = null;
		private CanvasGroup _group = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Manager"></param>
		public override void Initialize(SplashManager Manager)
		{
			handler = Manager;
			_group = GetComponent<CanvasGroup>();
			Hide();
		}

		/// <summary>
		/// 
		/// </summary>
		public override void ResetUI()
		{
			Hide();
			if (_sequence != null)
			{
				_sequence.Kill();
				_sequence = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="callBack"></param>
		public override void Play(System.Action callBack)
		{
			_sequence = DOTween
				.Sequence()
				.AppendCallback(()=> 
					{
						gameObject.SetActiveOptimized(true);
					});

			void Insert(VolumeFrame f)
			{
#if UNITY_POST_PROCESSING_STACK_V2
				_sequence.Insert(f.time, DOTween.To(()=> ppV.weight, x=> ppV.weight = x, f.weight, f.duration).SetEase(f.ease));
#endif
				_sequence.Insert(f.time, _group.DOFade(f.alpha, f.duration).SetEase(f.ease));
				if(f.playAudio)
				{
					_sequence.InsertCallback(f.time, ()=> { handler.PlayAudio(f.effectAudio); });
				}
			}

			for (int i = 0; i < Frames.Length; i++)
			{
				Insert(Frames[i]);
			}
			_sequence.AppendCallback(() => 
			{
				Hide();
				callBack?.Invoke();
			});
		}

		/// <summary>
		/// 
		/// </summary>
		private void Hide()
        {
			gameObject.SetActiveOptimized(false);
#if UNITY_POST_PROCESSING_STACK_V2
			ppV.weight = 0;
#endif
		}

		/// <summary>
		/// 
		/// </summary>
		private void OnDisable()
		{
			ResetUI();
		}

		[System.Serializable]
		public class VolumeFrame
		{
			[SerializeField] public float time;
			[SerializeField] public float duration;
			[SerializeField] public Ease ease;
			[SerializeField] public float weight;
			[SerializeField] public float alpha;
			[SerializeField] public bool playAudio;
			[SerializeField] public AudioClip effectAudio;
		}
	}
}