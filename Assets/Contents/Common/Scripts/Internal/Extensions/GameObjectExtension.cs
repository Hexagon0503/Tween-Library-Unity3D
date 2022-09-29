using UnityEngine;
using System.Collections;

public static class GameObjectExtension
{
    public static void SetActiveOptimized(this GameObject go, bool active)
    {
        if (active)
        {
            if (!go.activeSelf) go.SetActive(true);
        }
        else
        {
            if (go.activeSelf) go.SetActive(false);
        }
    }

	public static void SetActive(this GameObject[] go, bool active)
	{
		for (int i = 0; i < go.Length; i++)
		{
			go[i].SetActive(active);
		}
	}
}
