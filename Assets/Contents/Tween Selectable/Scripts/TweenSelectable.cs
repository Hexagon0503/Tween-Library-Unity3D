using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Tween
{
	public class TweenSelectable : Selectable
	{
		#region SERIALIZE FIELD
		[SerializeField] private TweenSelectableSettings tweenInfo = null;
        #endregion
        
        #region PRIVATE MEMBERS
        private Sequence currentSequence = null;
        #endregion

        #region UNITY METHODS
        /// <summary>
        /// 
        /// </summary>
        protected override void OnDisable()
		{
			base.OnDisable();
			StopTween();
		}
        #endregion

        #region SELECTABLE
        /// <summary>
        /// Transition the Selectable to the entered state.
        /// </summary>
        /// <param name="state">State to transition to</param>
        /// <param name="instant">Should the transition occur instantly.</param>
        protected override void DoStateTransition(SelectionState state, bool instant)
		{
			StopTween();
			PlayTween((SelectableState)(int)state);
		}
		/// <summary>
		/// Clear any internal state from the Selectable (used when disabling).
		/// </summary>
		protected override void InstantClearState()
		{
			StopTween();
			PlayTween(SelectableState.Normal);
		}
        #endregion

        #region TWEEN
        /// <summary>
        /// 
        /// </summary>
        private void PlayTween(SelectableState key)
		{
			currentSequence = tweenInfo.BuildSequence(key);
			if (currentSequence != null)
			{
				currentSequence.Play();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void StopTween()
		{
			if (currentSequence != null)
			{
				currentSequence.Kill();
				currentSequence = null;
			}
		}
        #endregion
    }
}