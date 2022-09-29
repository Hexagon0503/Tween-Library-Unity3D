using UnityEngine;
using UnityEngine.Events;

namespace Game.SplashScreen
{
	public class SplashManager : MonoBehaviour
	{
        #region SERIALIZE FIELD
        [Header("Settings")]
		public bool showOnStart = true;
		[Header("Elements")]
		public GameObject Root;
		public GameObject skipIndicator;
		[Header("Splash List")]
		public SplashScreenBase[] splashList;
		[Header("Events")]
		public UnityEvent onComplete;
        #endregion
        
        #region PRIVATE MEMBERS
        private int currentID = 0;
		private AudioSource aSource = null;
        #endregion

        #region UNITY METHODS
        /// <summary>
        ///
        /// </summary>
        private void Awake()
		{
			aSource = GetComponent<AudioSource>();
			Initialize();
		}

		/// <summary>
		/// 
		/// </summary>
        private void Start()
        {
            if(showOnStart)
            {
				Show();
            }
        }
        #endregion

        #region SPLASH
        /// <summary>
        ///
        /// </summary>
        void Initialize()
		{
			for (int i = 0; i < splashList.Length; i++)
			{
				splashList[i].Initialize(this);
			}
		}

		/// <summary>
		///
		/// </summary>
		public void Show()
		{
			Root.SetActive(true);
			currentID = 0;
			PlaySequence();
		}

		/// <summary>
		///
		/// </summary>
		public void Skip()
		{
			if(!canSkip) return;
			//
			skipIndicator.SetActiveOptimized(false);
			SwitchNext();
		}

		/// <summary>
		///
		/// </summary>
		void SwitchNext()
		{
			if(currentID >= splashList.Length) return;
			//
			splashList[currentID].ResetUI();
			currentID++;
			if(currentID >= splashList.Length )
			{
				OnFinish();
			}
			else
			{
				PlaySequence();
			}
		}

		/// <summary>
		///
		/// </summary>
		void PlaySequence()
		{
			if(currentID >= splashList.Length) return;
			//
			splashList[currentID].Play(SwitchNext);
		}

		/// <summary>
		///
		/// </summary>
		public void StopSplash()
		{
			ResetUI();
		}
		/// <summary>
		///
		/// </summary>
		void OnFinish()
		{
			Root.SetActive(false);
			onComplete?.Invoke();
			ResetUI();
		}

		/// <summary>
		///
		/// </summary>
		public void ResetUI()
		{
			for (int i = 0; i < splashList.Length; i++)
			{
				splashList[i].ResetUI();
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="audio"></param>
		public void PlayAudio(AudioClip audio)
		{
			if(aSource == null || audio == null)
				return;
			aSource.PlayOneShot(audio);
		}
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// 
        /// </summary>
        private bool enableSkip = true;
		public bool canSkip
		{
			get=> enableSkip;
			set
			{
				skipIndicator.SetActiveOptimized(value);
				enableSkip = value;
			}
		}
        #endregion
    }
}