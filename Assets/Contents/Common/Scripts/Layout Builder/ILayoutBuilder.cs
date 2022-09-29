using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ILayoutBuilder : MonoBehaviour
{
	public bool buildOnStart = true;

	protected virtual void Start()
	{
		if(buildOnStart)
		{
			Build();
		}
	}

	public virtual void Build()
	{
	}

	private RectTransform m_Rect = null;
	protected RectTransform _rectTransform
	{
		get
		{
			if (m_Rect == null)
			{
				m_Rect = GetComponent<RectTransform> ();
			}
			return m_Rect;
		}
	}
}
