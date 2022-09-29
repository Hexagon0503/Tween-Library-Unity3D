using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class bl_JoystickArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private bl_Joystick joystick;
	//
	private CanvasGroup stickAlpha;
	private bl_MobileButtonTween tweenEffect = null;
	private int lastID = -2;

	#region UNITY METHOD
	/// <summary>
	/// 
	/// </summary>
	private void Awake()
    {
		if(joystick == null)
        {
			Debug.LogError("[Error] : Joystick  Null.");
			return;
        }
		stickAlpha = joystick.GetComponent<CanvasGroup>();
		tweenEffect = joystick.GetComponent<bl_MobileButtonTween>();
    }
    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
		stickAlpha.interactable = false;
		stickAlpha.alpha = 0;
	}

	/// <summary>
	/// 
	/// </summary>
	private void OnDisable()
	{
		stickAlpha.interactable = true;
		stickAlpha.alpha = 1;
		joystick.ResetToDefaultPosition();
	}
	#endregion

	#region POINTER EVENTS
	/// <summary>
	/// 
	/// </summary>
	public void OnPointerDown(PointerEventData data)
	{
		if (lastID == -2)
		{
			lastID = data.pointerId;
			joystick.RootRect.position = data.pressPosition;
			stickAlpha.alpha = 1;
			joystick.OnPointerDown(data);
			if (tweenEffect != null)
				tweenEffect.OnPointerDown(data);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void OnDrag(PointerEventData data)
	{
		if (data.pointerId == lastID)
		{
			joystick.OnDrag(data);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void OnPointerUp(PointerEventData data)
	{
		if (data.pointerId == lastID)
		{
			joystick.OnPointerUp(data);
			if (tweenEffect != null)
				tweenEffect.OnPointerUp(data);
			if (!joystick.IsSprintLocked)
            {
				stickAlpha.alpha = 0;
				joystick.ResetToDefaultPosition();
			}
			lastID = -2;
		}
	}
	#endregion
}
