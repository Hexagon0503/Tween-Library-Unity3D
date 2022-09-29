using UnityEngine;
using UI.Tween;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;

public class bl_MobileButtonTween : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool interactable = true;
    public bool Interactable
    {
        get => interactable;
        set
        {
            if(value != interactable)
            {
                interactable = value;
                if (interactable)
                {
                    ChangeState(SelectableState.Normal);
                }
                else
                {
                    ChangeState(SelectableState.Disabled);
                }
            }
        }
    }

    private bool selected = false;
    public bool Selected
    {
        get => selected;
        set
        {
            if (!Interactable) return;
            //
            if (value != selected)
            {
                selected = value;
                if (lastID == -2)
                {
                    if (selected)
                    {
                        ChangeState(SelectableState.Selected);
                    }
                    else
                    {
                        ChangeState(SelectableState.Normal);
                    }
                }
            }
        }
    }

    [SerializeField] private TweenSelectableSettings tweenInfo;
    //
    private Dictionary<SelectableState, Sequence> savedSequences = null;
    private Sequence currentSequence = null;
    private int lastID = -2;

    #region UNITY METHOD
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        BuildSequence();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        if (Selected)
        {
            ChangeState(SelectableState.Pressed);
        }
        else
        {
            ChangeState(SelectableState.Normal);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        lastID = -2;
        ResetSequence();
    }
    #endregion

    #region TWEEEN
    /// <summary>
    /// 
    /// </summary>
    private void BuildSequence()
    {
        savedSequences = new Dictionary<SelectableState, Sequence>();
        savedSequences.Add(SelectableState.Pressed, tweenInfo.BuildSequence(SelectableState.Pressed));
        savedSequences.Add(SelectableState.Selected, tweenInfo.BuildSequence(SelectableState.Selected));
        savedSequences.Add(SelectableState.Disabled, tweenInfo.BuildSequence(SelectableState.Disabled));
        savedSequences.Add(SelectableState.Normal, tweenInfo.BuildSequence(SelectableState.Normal));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    private void ChangeState(SelectableState key)
    {
        ResetSequence();
        currentSequence = savedSequences[key];
        currentSequence.Rewind();
        currentSequence.Restart();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetSequence()
    {
        if (currentSequence != null)
        {
            currentSequence.Pause();
            currentSequence = null;
        }
    }
    #endregion

    #region POINTER EVENTS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Interactable) return;

        if (lastID == -2)
        {
            lastID = eventData.pointerId;
            ChangeState(SelectableState.Pressed);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Interactable) return;

        if (lastID == eventData.pointerId)
        {
            lastID = -2;
            if (Selected)
            {
                ChangeState(SelectableState.Selected);
            }
            else
            {
                ChangeState(SelectableState.Normal);
            }
        }
    }
    #endregion
}
