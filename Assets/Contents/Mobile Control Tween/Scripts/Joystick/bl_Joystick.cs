using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bl_Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Settings")]
    [SerializeField, Range(0.1f, 2f)] private float edgeWidth = 0.5f;
    [SerializeField, Range(0.1f, 2f)] private float maxMagnitude = 1f;

    [Header("Run Marker")]
    [SerializeField, Range(0.1f, 2f)] private float sprintLockInput = 1.8f;
    [SerializeField] private GameObject runIndicatorUI;
    [SerializeField] private GameObject sprintLockedUI;

    [Header("Direction Marker")]
    [SerializeField, Range(0.1f, 2f)] private float directionMultiplier = 1f;
    [SerializeField] private CanvasGroup directionGroup;

    [Header("Reference")]
    [SerializeField] private RectTransform stickRect;
    [SerializeField] private RectTransform rootRect;
    [SerializeField] private Image backImage;

    //Privates
    #region Private Members
    private int lastId = -2;
    private Vector3 defaultPosition;
    private bool isSprintLocked = false;
    #endregion

    #region Unity Method
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        defaultPosition = rootRect.anchoredPosition;
        if (stickRect == null)
        {
            Debug.LogError("Please add the stick for joystick work!.");
            this.enabled = false;
            return;
        }
    }
    #endregion

    #region Unity Pointer Events
    /// <summary>
    /// When click here event
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerDown(PointerEventData data)
    {
        //Detect if is the default touchID
        if (lastId == -2)
        {
            //then get the current id of the current touch.
            //this for avoid that other touch can take effect in the drag position event.
            //we only need get the position of this touch
            lastId = data.pointerId;
            OnDrag(data);
            if(isSprintLocked)
            {
                isSprintLocked = false;
                sprintLockedUI.SetActive(false);
                runIndicatorUI.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    private float rawAngle;
    public void OnDrag(PointerEventData data)
    {
        //If this touch id is the first touch in the event
        if (data.pointerId == lastId)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backImage.rectTransform, data.position, null, out pos))
            {
                pos.x = (pos.x / backImage.rectTransform.sizeDelta.x);
                pos.y = (pos.y / backImage.rectTransform.sizeDelta.y);

                _inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
                _inputVector = (_inputVector.magnitude > maxMagnitude) ? _inputVector.normalized * maxMagnitude : _inputVector;
                stickRect.anchoredPosition = new Vector3(_inputVector.x * (backImage.rectTransform.sizeDelta.x * edgeWidth), _inputVector.y * (backImage.rectTransform.sizeDelta.y * edgeWidth));
                //
                rawAngle = Mathf.Atan2(data.position.y - rootRect.position.y, data.position.x - rootRect.position.x) * Mathf.Rad2Deg;

                directionGroup.alpha = Mathf.Clamp(_inputVector.sqrMagnitude * directionMultiplier, 0, 1);
                directionGroup.transform.localRotation = Quaternion.Euler(0, 0, rawAngle + 270);
                //
                runIndicatorUI.SetActive(_inputVector.y > 1);
            }
        }
    }

    /// <summary>
    /// When touch is Up
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerUp(PointerEventData data)
    {
        //leave the default id again
        if (data.pointerId == lastId)
        {
            if(!isSprintLocked && InputVector.y > sprintLockInput)
            {
                isSprintLocked = true;
                sprintLockedUI.SetActive(true);
                runIndicatorUI.SetActive(true);
            }
            else
            {
                runIndicatorUI.SetActive(false);
            }
            //-2 due -1 is the first touch id
            lastId = -2;
            stickRect.anchoredPosition = Vector3.zero;
            _inputVector = Vector2.zero;

            directionGroup.alpha = 0;
        }
    }
    #endregion

    public void OverrideDefaultPosition(Vector2 newPos)
    {
        defaultPosition = newPos;
        ResetToDefaultPosition();
    }
    public void ResetToDefaultPosition()
    {
        rootRect.anchoredPosition = defaultPosition;
    }
    #region GET
    /// <summary>
    /// Input Value
    /// </summary>
    private Vector2 _inputVector;
    public Vector2 InputVector
    {
        get
        {
            return _inputVector;
        }
    }
    /// <summary>
    /// Value Horizontal of the Joystick
    /// Get this for get the horizontal value of joystick
    /// </summary>
    public float Horizontal
    {
        get
        {
            return _inputVector.x;
        }
    }

    /// <summary>
    /// Value Vertical of the Joystick
    /// Get this for get the vertical value of joystick
    /// </summary>
    public float Vertical
    {
        get
        {
            if (isSprintLocked)
            {
                return 2;
            }
            return _inputVector.y;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public RectTransform RootRect => rootRect;
    public bool IsSprintLocked => isSprintLocked;
    #endregion
}