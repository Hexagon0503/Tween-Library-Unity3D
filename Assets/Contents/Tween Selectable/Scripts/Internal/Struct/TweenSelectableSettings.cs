using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Tween
{
	[System.Serializable]
	public class TweenSelectableSettings
	{
		[Range(0.1f, 0.5f)]public float tweenDuration = 0.15f;
		public TweenSelectableFrame[] Frames;

		public Sequence BuildSequence(SelectableState state, bool instant = false)
		{
			var sequence = DOTween.Sequence ();
			//
			for (int i = 0; i < Frames.Length; i++)
			{
				Frames[i].AddSequence(sequence, !instant ? tweenDuration : 0, state);
			}
			sequence.SetAutoKill(false);
			sequence.SetRecyclable(true);
			sequence.Pause();
			return sequence;
		}
	}

	[System.Serializable]
	public class TweenSelectableFrame
	{
		public TweenSelectionProfile profile;
		public float tweenTine=> profile.time;
		[Space]
		public Graphic[] m_Graphics;
		public RectTransform rectPoint;

		public Sequence AddSequence(Sequence sequence, float duration, SelectableState state)
		{
			float targetAlpha, targetScale;
			Color targetColor;
			switch (state)
			{
			default:
				targetAlpha = profile.alphaFrames.normalValue;
				targetScale = profile.scaleFrames.normalValue;
				targetColor = profile.colorFrames.normalColor;
				break;
			case SelectableState.Highlighted:
				targetAlpha = profile.alphaFrames.highlightValue;
				targetScale = profile.scaleFrames.highlightValue;
				targetColor = profile.colorFrames.highlightedColor;
				break;
			case SelectableState.Pressed:
				targetAlpha = profile.alphaFrames.pressedValue;
				targetScale = profile.scaleFrames.pressedValue;
				targetColor = profile.colorFrames.pressedColor;
				break;
			case SelectableState.Selected:
				targetAlpha = profile.alphaFrames.selectedValue;
				targetScale = profile.scaleFrames.selectedValue;
				targetColor = profile.colorFrames.selectedColor;
				break;
			case SelectableState.Disabled:
				targetAlpha = profile.alphaFrames.disabledValue;
				targetScale = profile.scaleFrames.disabledValue;
				targetColor = profile.colorFrames.disabledColor;
				break;
			}
			for (int i = 0; i < m_Graphics.Length; i++)
			{
				if (profile.useAlpha)
				{
					sequence.Insert (tweenTine, m_Graphics[i].DOFade (targetAlpha, duration).SetEase (profile.ease));
				}
				if (profile.useColor)
				{
					sequence.Insert(tweenTine, m_Graphics[i].DOColor (targetColor, profile.colorFrames.fadeDuration).SetEase (profile.ease));
				}
			}
			if (profile.useScale) 
			{
				sequence.Insert(tweenTine, rectPoint.DOScale (targetScale, duration).SetEase (profile.ease));
			}
			return sequence;
		}
	}

	[System.Serializable]
	public class FloatBlock
	{
		public float normalValue = 1f;
		public float highlightValue = 1f;
		public float pressedValue = 0.75f;
		public float selectedValue = 1f;
		public float disabledValue = 0.75f;
	}
}