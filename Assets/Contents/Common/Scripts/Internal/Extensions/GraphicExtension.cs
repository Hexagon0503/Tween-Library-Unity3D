using UnityEngine;
using UnityEngine.UI;

public static class GraphicExtension
{
	public static void SetColor(this Graphic[] colorUI, Color _color)
	{
		for (var i = 0; i < colorUI.Length; i++)
		{
			colorUI[i].color = _color;
		}
	}
}
