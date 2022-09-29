using UnityEngine;
using UnityEngine.UI;

public static class CanvasExtensions
{
    /// <summary>
    /// Converts a world space position into a canvas space position
    /// </summary>
    /// <param name="Canvas"></param>
    /// <param name="World Position"></param>
    /// <param name="Current Camera"></param>
    /// <returns></returns>
    public static Vector3 WorldToCanvas(this Canvas canvas, Vector3 world_position, Camera camera = null)
    {
        if (camera == null)
        {
            return Vector3.zero;
        }
        var viewport_position = camera.WorldToViewportPoint(world_position);
        var canvas_rect = canvas.GetComponent<RectTransform>();
        return new Vector3((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f), (viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f), viewport_position.z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="anchoredPosition"></param>
    /// <returns></returns>
    public static bool IsVisible(this Canvas canvas, Vector3 canvasPos)
    {
        return canvasPos.z > 0;
    }
}