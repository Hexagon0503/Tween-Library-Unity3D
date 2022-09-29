using UnityEngine;

namespace Game.SplashScreen
{
	public abstract class SplashScreenBase : MonoBehaviour
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Manager"></param>
		public abstract void Initialize(SplashManager Manager);

		/// <summary>
		/// 
		/// </summary>
		public abstract void ResetUI();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="callBack"></param>
		public abstract void Play (System.Action callBack);
	}
}