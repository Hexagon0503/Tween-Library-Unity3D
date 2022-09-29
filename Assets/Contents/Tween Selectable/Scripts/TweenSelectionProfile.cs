using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Tween
{
	[CreateAssetMenu(fileName = "UI Tween Frame", menuName = "Interface/Selectable/Frame Profile", order = 1)]
	public class TweenSelectionProfile : ScriptableObject
	{
        #region SERIALIZE FIELD
        public float time;
		[Header("Transition")]
		public bool useColor;
		public ColorBlock colorFrames;
		public bool useAlpha;
		public FloatBlock alphaFrames;
		public bool useScale;
		public FloatBlock scaleFrames;
		public Ease ease;
        #endregion
    }
}