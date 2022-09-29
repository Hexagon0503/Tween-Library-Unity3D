using UnityEngine;
using UnityEngine.UI;

public class UIImageBuilder : ILayoutBuilder
{
	public LayoutType buildType;
	//
	private Vector2 targetSize = Vector2.zero;
	private float reduceAmount;

	public override void Build()
	{
		reduceAmount = layoutElement.preferredHeight / Icon.preferredHeight;
		targetSize.x = Icon.preferredWidth * reduceAmount;
		targetSize.y = Icon.preferredHeight * reduceAmount;
		switch(buildType)
		{
		case LayoutType.SizeDelta:
			_rectTransform.sizeDelta = targetSize;
			break;
		case LayoutType.Mininum:
			layoutElement.minWidth = targetSize.y;
			layoutElement.minHeight = targetSize.y;
			break;
		case LayoutType.Preferrred:

			layoutElement.preferredWidth = targetSize.y;
			layoutElement.preferredHeight = targetSize.y;
			break;
		}
	}

	private Image _Icon = null;
	private Image Icon
	{
		get
		{
			if(_Icon == null)
			{
				_Icon = GetComponent<Image>();
			}
			return _Icon;
		}
	}

	private LayoutElement _layoutElement = null;
	private LayoutElement layoutElement
	{
		get
		{
			if(_layoutElement == null)
			{
				_layoutElement = GetComponent<LayoutElement>();
			}
			return _layoutElement;
		}
	}

	public enum LayoutType
	{
		SizeDelta = 0,
		Mininum = 1,
		Preferrred = 2,
	}
}
