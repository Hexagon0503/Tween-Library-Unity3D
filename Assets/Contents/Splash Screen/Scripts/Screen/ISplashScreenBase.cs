using UnityEngine;

namespace Game.SplashScreen
{
	public abstract class ISplashScreenBase : MonoBehaviour
	{
		public abstract void Initialize(SplashManager Manager);
		public abstract void Reset();
		public abstract void Play (System.Action callBack);
	}
}